
import {action} from 'mobx';
import {postSendConfirmationCode} from '~/services/confirmation-code/confirmation-code.service';
import {BaseStore, CartDataStore} from '~/stores';
import {onlyNumbersMask} from '~/utils';

type PreferredMedium = ContactPreferencePage.Form.PreferredMedium;

class Store extends BaseStore<ContactPreferencePageStore.State> {

  constructor() {
    super({
      isLoading: false,
    });
  }

  @action
  public async sendCode(preferredMedium: PreferredMedium) {
    this.setState(state => {
      state.isLoading = true;
    });

    const {selectedOwner: {email, phone}} = CartDataStore.state;

    CartDataStore.setState(state => {
      state.contactType = preferredMedium;
    });

    try {
      await postSendConfirmationCode({
        contactType: preferredMedium,
        resend: false,
        email,
        phone: onlyNumbersMask(phone),
      });
    } catch(error) {
      throw error;
    } finally {
      this.setState(state => {
        state.isLoading = false;
      });
    }
  }

}

export const ContactPreferencePageStore = new Store();
