import {action} from 'mobx';
import {BaseStore, CartDataStore} from '~/stores';

type FromValues = EstablishmentDataPage.Form.FormValues;

class Store extends BaseStore<EstablishmentDataPageStore.State> {

  constructor() {
    super({isLoading: false});
  }

  @action
  public async submitData(values: FromValues): Promise<void> {
    this.setState(state => {
      state.isLoading = true;
    });
    CartDataStore.setState(state => {
      state.establishment = {
        ...state.establishment,
        name: values.companyName,
        tradingName: values.tradingName,
        phone: values.establishmentPhone,
        address: {
          ...state.establishment.address,
          additionalInfo: values.additionalInfo,
          city: values.city,
          neighborhood: values.neighborhood,
          number: values.number,
          state: values.state,
          uf: values.state,
          street: values.street,
          zipCode: values.zipCode,
        },
      };
    });

    try {
      await CartDataStore.update();
    } catch (error) {
      throw error;
    } finally {
      this.setState(state => {
        state.isLoading = false;
      });
    }
  }
}

export const EstablishmentDataPageStore = new Store();
