
import {action} from 'mobx';
import {patchConfirmCode} from '~/services/confirmation-code/confirmation-code.service';
import {BaseStore, CartDataStore} from '~/stores';
import {onlyNumbersMask} from '~/utils';

type State = ConfirmationCodePageStore.State;

class Store extends BaseStore<State> {

  constructor() {
    super({
      isLoading: false,
    });
  }

  @action
  public async confirmCode(code: string): Promise<void> {
    this.setState(state => {
      state.isLoading = true;
    });

    const {selectedOwner: {email, phone}, contactType} = CartDataStore.state;

    try {
      await patchConfirmCode({
        contactType,
        code,
        email,
        phone: onlyNumbersMask(phone),
      });
    } catch (error) {
      throw error;
    } finally {
      this.setState(state => {
        state.isLoading = false;
      });
    }
  }
}

export const ConfirmationCodePageStore = new Store();
