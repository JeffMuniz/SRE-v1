import {action} from 'mobx';
import {getFeesAndTariffs} from '~/services/fees/fees-and-tariffs.service';
import {BaseStore, CartDataStore} from '~/stores';
import {onlyNumbersMask} from '~/utils';

type GetFeesAndTariffsDTO = FeesAndTariffsService.GetFeesAndTariffsDTO;

class Store extends BaseStore<FeesAndTariffsPageStore.State> {

  constructor() {
    super({
      isLoading: false,
    });
  }

  @action
  public async getFees(): Promise<void> {
    this.setState(state => {
      state.isLoading = true;
    });

    const {establishment: {cnpj}} = CartDataStore.state;

    let response: GetFeesAndTariffsDTO;

    try {
      response = await getFeesAndTariffs({cnpj: onlyNumbersMask(cnpj)});
    } catch (error) {
      throw error;
    } finally {
      this.setState(state => {
        state.isLoading = false;
      });
    }

    CartDataStore.setState(state => {
      state.fees = response;
    });
  }
}

export const FeesAndTariffsPageStore = new Store();
