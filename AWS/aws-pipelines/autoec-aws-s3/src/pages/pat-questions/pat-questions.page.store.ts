
import {action} from 'mobx';
import {BaseStore, CartDataStore} from '~/stores';

type State = PATQuestionsPageStore.State;
type FormValues = PATQuestionsPage.FormValues;

class Store extends BaseStore<State> {

  constructor() {
    super({
      showTermsModal: false,
      showRequirementsModal: false,
      isLoading: false,
    });
  }

  @action
  public async updatePatQuestions(values: FormValues): Promise<void> {
    CartDataStore.setState(state => {
      state.patQuestions = {
        cashRegisters: this.optionToRange(values.cashRegisters),
        dailyServings: this.optionToRange(values.dailyServings),
        diningPlaces: this.optionToRange(values.diningPlaces),
        fruitOnMenu: this.yesOrNoToBoolean(values.fruitOnMenu),
        servingArea: this.optionToRange(values.servingArea),
      };
      state.products = {
        va: this.yesOrNoToBoolean(values.vaAcceptance),
        vr: this.yesOrNoToBoolean(values.vrAcceptance),
      };
    });
  }

  private optionToRange(option: string): { min: number; max: number } {
    if(!option) return {min: null, max: null};

    const [ min, max ] = option.split('-').map(item => {
      const result = parseInt(item);
      if(isNaN(result)) {
        return null;
      }
      return result;
    });
    return {min, max};
  };

  private yesOrNoToBoolean(yesOrNo: PATQuestionsPage.YesOrNo): boolean {
    switch (yesOrNo) {
      case 'yes': return true;
      case 'no': return false;
      default: return null;
    }
  }
}

export const PATQuestionsPageStore = new Store();
