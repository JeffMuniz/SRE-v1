
import {BaseStore} from '~/stores';

class Store extends BaseStore<{ }> {

  constructor() {
    super({ });
  }

}

export const CPFConfirmationPageStore = new Store();
