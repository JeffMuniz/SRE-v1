
import {action} from 'mobx';
import {BaseStore} from '~/stores';
import {CartDataStore} from '~/stores';

type State = DataConfirmationPageStore.State;
type FormValues = DataConfirmationPage.Form.FormValues;

class Store extends BaseStore<State> {

  constructor() {
    super({isLoading: false});
  }

  @action
  public async finishOrder(values: FormValues): Promise<void> {
    this.setState(state => {
      state.isLoading = true;
    });

    CartDataStore.setState(state => {
      state.selectedOwner = {
        ...state.selectedOwner,
        email: values.email,
        phone: values.phone,
      };
      state.establishment = {
        ...state.establishment,
        tradingName: values.tradingName,
        phone: values.establishmentPhone,
        address: {
          ...state.establishment.address,
          zipCode: values.zipCode,
          additionalInfo: values.additionalInfo,
          city: values.city,
          neighborhood: values.neighborhood,
          number: values.number,
          state: values.state,
          street: values.street,
          uf: values.state,
        },
      };
      state.bankAccount = {
        ...state.bankAccount,
        account: values.account,
        agency: values.agency,
        bank: values.bank,
        digit: values.digit,
      };
    });

    try {
      await CartDataStore.update();
      await CartDataStore.finish();
    } catch (error) {
      throw error;
    } finally {
      this.setState(state => {
        state.isLoading = false;
      });
    }
  }

}

export const DataConfirmationPageStore = new Store();
