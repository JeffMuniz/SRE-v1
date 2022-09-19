
import {action} from 'mobx';
import {getEstablishmentDetail} from '~/services';
import {BaseStore, CartDataStore} from '~/stores';
import {onlyNumbersMask} from '~/utils';

type GetDetailDTO = EstablishmentService.GetDetailDTO;

class Store extends BaseStore<HomePageStore.State> {

  constructor() {
    super({
      isLoading: false,
    });
  }

  @action
  public async getCnpjStatus(cnpj: string): Promise<CartDataStore.EstablishmentStatus> {
    this.setState(state => {
      state.isLoading = true;
    });

    CartDataStore.setState(state => {
      state.establishment.cnpj = cnpj;
    });

    const cleanedCnpj = onlyNumbersMask(cnpj);
    let response: GetDetailDTO;

    try {
      response = await getEstablishmentDetail({cnpj: cleanedCnpj});

      CartDataStore.setState(state => {
        state.establishment = {
          ...state.establishment,
          status: response.status,
          name: response.name,
          owners: response.owners,
          tradingName: response.tradingName,
          address: response.address,
        };
      });

      return response.status;
    } catch (error) {
      throw error;
    } finally {
      this.setState(state => {
        state.isLoading = false;
      });
    }
  }

}

export const HomePageStore = new Store();
