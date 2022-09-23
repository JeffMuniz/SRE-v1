
import {action} from 'mobx';
import {getOwnerDetail} from '~/services/';
import {postOwnerIdentityConfirmation} from '~/services/owner-information/owner-information.service';
import {BaseStore, CartDataStore} from '~/stores';
import {onlyNumbersMask} from '~/utils';

type FormValues = IdentityConfirmationPage.Form.FormValues;
type GetOwnerDetailDTO = OwnerInformationService.GetOwnerDetailDTO;

class Store extends BaseStore<IdentityConfirmationPageStore.State> {

  constructor() {
    super({
      showPageLoading: false,
      showErrorModal: false,
      showButtonLoading: false,
      options: {
        motherNames: [],
        birthDates: [],
      },
    });
  }

  @action
  public async getOwnerInformation() {
    this.setState(state => {
      state.showPageLoading = true;
    });

    const {
      selectedOwner: {cpf},
      establishment: {cnpj},
    } = CartDataStore.state;

    let response: GetOwnerDetailDTO;

    try {
      response = await getOwnerDetail({
        cnpj: onlyNumbersMask(cnpj),
        cpf: onlyNumbersMask(cpf),
      });
    } catch (error) {
      throw error;
    } finally {
      this.setState(state => {
        state.showPageLoading = false;
      });
    }

    this.setState(state => {
      state.options = {
        ...state.options,
        motherNames: response.motherNames,
        birthDates: response.birthDates,
      };
    });
  }

  @action
  public async confirmIdentity(values: FormValues) {
    this.setState(state => {
      state.showButtonLoading = true;
    });

    const {
      selectedOwner: {cpf, name},
      establishment: {cnpj},
    } = CartDataStore.state;

    let id: string;

    try {
      id = await postOwnerIdentityConfirmation({
        cpf: onlyNumbersMask(cpf),
        cnpj: onlyNumbersMask(cnpj),
        name,
        motherName: values.motherName,
        birthDate: values.birthday,
      });

    } catch (error) {
      throw error;
    } finally {
      this.setState(state => {
        state.showButtonLoading = false;
      });
    }

    CartDataStore.setState(state => {
      state.id = id;
    });
  }
}

export const IdentityConfirmationPageStore = new Store();
