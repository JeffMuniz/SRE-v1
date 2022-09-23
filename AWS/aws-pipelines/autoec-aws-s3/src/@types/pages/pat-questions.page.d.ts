
declare namespace PATQuestionsPage {

  type Question =
  'vr-acceptance' |
  'va-acceptance' |
  'dining-places' |
  'daily-servings' |
  'serving-area' |
  'fruit-on-menu' |
  'cash-registers';

  type RouteMatchParams = {
    question: Question;
  }

  type YesOrNo = 'yes' | 'no' | '';
  type DiningPlaces = '1-30' | '31-60' | '61-99' | '100-or-more' | '';
  type DailyServings = '0-100' | '101-200' | '201-299' | '300-or-more' | '';
  type ServingArea = '0-50' | '51-100' | '101-499' | '500-or-more' | '';
  type CashRegisters = '1-30' | '31-60' | '61-99' | '100-or-more' | '';

  type FormValues = {
    vrAcceptance: YesOrNo;
    vaAcceptance: YesOrNo;
    diningPlaces: DiningPlaces;
    dailyServings: DailyServings;
    servingArea: ServingArea;
    fruitOnMenu: YesOrNo;
    cashRegisters: CashRegisters;
  }

  namespace TermsModal {
    type State = {
      isLoading: boolean;
    };
    type FormValues = {
      nutritionalTerm: boolean;
      truthfulInfoTerm: boolean;
    };
  }
}
