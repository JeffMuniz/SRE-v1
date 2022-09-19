
import {action} from 'mobx';
import {patchCartOrder} from '~/services';
import {BaseStore} from '~/stores/base/base.store';
import {deepClone, isEqual} from '~/utils';

class Store extends BaseStore<CartDataStore.State> {

  constructor() {
    super({
      acquirers: [ ],
      bankAccount: {
        account: '',
        agency: '',
        bank: '',
        digit: '',
      },
      creationDate: null,
      establishment: {
        address: {
          additionalInfo: '',
          city: '',
          neighborhood: '',
          number: '',
          state: '',
          street: '',
          zipCode: '',
          uf: '',
        },
        status: null,
        cnpj: '',
        name: '',
        phone: '',
        tradingName: '',
        owners: [ ],
      },
      id: null,
      selectedOwner: {
        cpf: '',
        email: '',
        name: '',
        phone: '',
      },
      patQuestions: {
        cashRegisters: {
          max: null,
          min: null,
        },
        dailyServings: {
          max: null,
          min: null,
        },
        diningPlaces: {
          max: null,
          min: null,
        },
        fruitOnMenu: null,
        servingArea: {
          max: null,
          min: null,
        },
      },
      products: {
        vr: null,
        va: null,
      },
      termsAcceptance: {
        nutritional: null,
        truthfulInfo: null,
      },
      updateDate: null,
      contactType: null,
      fees: [ ],
    }, {
      persistence: {
        key: 'cart',
        storage: localStorage,
        expiresIn: 1000 * 60 * 60, // 1 hour.
      },
    });
  }

  @action
  public async update(): Promise<void> {
    const cleared = this.getClearedState();
    await patchCartOrder(cleared);
  }

  @action
  public async finish(): Promise<void> {
    await new Promise(resolve => setTimeout(resolve, 1000));
    this.resetState();
  }

  private getClearedState(): CartDataStore.State {
    const state = deepClone(this.state);
    Object.keys(state).forEach(k => {
      const prev = (this.initialState as any)[k];
      const next = (state as any)[k];
      if(isEqual(prev, next)) delete (state as any)[k];
    });

    return state;
  }

}

export const CartDataStore = new Store();
