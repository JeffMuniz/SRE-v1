
import {action} from 'mobx';
import {postSendConfirmationCode} from '~/services';
import {BaseStore, CartDataStore} from '~/stores';
import {onlyNumbersMask} from '~/utils';

class Store extends BaseStore<ContactInfoPageStore.State> {

  constructor() {
    super({isLoading: false});
  }

  @action
  public async submitData(values: ContactInfoPage.Form.FormValues) {
    this.state.isLoading = true;

    try {
      await postSendConfirmationCode({
        contactType: 'phone',
        resend: false,
        email: null,
        phone: onlyNumbersMask(values.phone),
      });
    } catch(error) {
      throw error;
    } finally {
      this.setState(state => {
        state.isLoading = false;
      });
    }

    CartDataStore.setState(state => {
      state.selectedOwner = {
        ...state.selectedOwner,
        email: values.email,
        phone: values.phone,
      };
      state.contactType = 'phone';
    });
  }

}

export const ContactInfoPageStore = new Store();
