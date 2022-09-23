
import {action} from 'mobx';
import {BaseStore, CartDataStore} from '~/stores';

type State = NameConfirmationPageStore.State;

class Store extends BaseStore<State> {

  constructor() {
    super({
      isLoading: false,
    });
  }

  @action
  public async confirmName(name: string, cpf: string): Promise<void> {
    CartDataStore.setState(state => {
      state.selectedOwner = {
        ...state.selectedOwner,
        name,
        cpf,
      };
    });
  }
}

export const NameConfirmationPageStore = new Store();
