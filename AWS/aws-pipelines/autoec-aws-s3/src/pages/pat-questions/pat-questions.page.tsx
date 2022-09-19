
import {FC, useCallback} from 'react';
import {Formik, FormikHelpers} from 'formik';
import {observer} from 'mobx-react-lite';
import {useHistory, useRouteMatch} from 'react-router-dom';
import {ROUTES} from '~/consts';
import {CartDataStore} from '~/stores';
import {
existsValidation, isNullOrUndefined, validationBase,
} from '~/utils';
import CashRegisters from './cash-registers/cash-registers';
import DailyServings from './daily-servings/daily-servings';
import DiningPlaces from './dining-places/dining-places';
import FruitOnMenu from './fruit-on-menu/fruit-on-menu';
import {PATQuestionsPageStore} from './pat-questions.page.store';
import {
 Form, PQSpacer, SubmitButton, Wrapper,
} from './pat-questions.page.styles';
import RequirementsModal from './requirements-modal/requirements-modal';
import ServingArea from './serving-area/serving-area';
import TermsModal from './terms-modal/terms-modal';
import VAAcceptance from './va-acceptance/va-acceptance';
import VRAcceptance from './vr-acceptance/vr-acceptance';

type CashRegistersType = PATQuestionsPage.CashRegisters;
type DailyServingsType = PATQuestionsPage.DailyServings;
type DiningPlacesType = PATQuestionsPage.DiningPlaces;
type FormHelpers = FormikHelpers<FormValues>;
type FormValues = PATQuestionsPage.FormValues;
type RouteMatchParams = PATQuestionsPage.RouteMatchParams;
type ServingAreaType = PATQuestionsPage.ServingArea;
type YesOrNo = PATQuestionsPage.YesOrNo;

const route = `${ROUTES.PAT_QUESTIONS}/:question`;

const PATQuestionsPage: FC = () => {

  const history = useHistory();
  const {params: {question}} = useRouteMatch<RouteMatchParams>(route);

  const navigateNext = useCallback(({
    vrAcceptance,
    vaAcceptance,
  }: {
    vrAcceptance: boolean;
    vaAcceptance: boolean;
  }) => {
    let nextQuestion: PATQuestionsPage.Question;
    if(vrAcceptance) {
      switch (question) {
        case 'vr-acceptance': nextQuestion = 'dining-places'; break;
        case 'dining-places': nextQuestion = 'daily-servings'; break;
        case 'daily-servings': nextQuestion = 'serving-area'; break;
        case 'serving-area': nextQuestion = 'fruit-on-menu'; break;
        case 'fruit-on-menu': nextQuestion = 'va-acceptance'; break;
        case 'va-acceptance': {
          if(vaAcceptance) nextQuestion = 'cash-registers';
          break;
        }
        default: break;
      }
    } else {
      switch (question) {
        case 'vr-acceptance': nextQuestion = 'va-acceptance'; break;
        case 'va-acceptance': {
          if(vaAcceptance) {
            nextQuestion = 'serving-area';
          } else {
            PATQuestionsPageStore.setState(state => {
              state.showRequirementsModal = true;
            });
            return;
          }
          break;
        }
        case 'serving-area': nextQuestion = 'cash-registers'; break;
        default: break;
      }
    }

    if(nextQuestion) {
      history.push(`${ROUTES.PAT_QUESTIONS}/${nextQuestion}`);
    } else {
      PATQuestionsPageStore.setState(state => {
        state.showTermsModal = true;
      });
    }
  }, [ history, question ]);

  const handleFormSubmission = useCallback(
    async(values: FormValues, helpers: FormHelpers) => {
    await PATQuestionsPageStore.updatePatQuestions(values);
    navigateNext({
      vaAcceptance: values.vaAcceptance === 'yes',
      vrAcceptance: values.vrAcceptance === 'yes',
    });
  }, [ navigateNext ]);

  const rangeToOption = <T extends unknown>({min, max}: {
    min: number;
    max: number;
  }): T => {
    return (isNullOrUndefined(min) ? '' : `${min}-${max || 'or-more'}`) as T;
  };

  const booleanToYesOrNo = (value: boolean): YesOrNo => {
    switch (value) {
      case true: return 'yes';
      case false: return 'no';
      default: return '';
    }
  };

  const {isLoading} = PATQuestionsPageStore.state;

  const {
    patQuestions: {
      cashRegisters,
      dailyServings,
      diningPlaces,
      fruitOnMenu,
      servingArea,
    },
    products,
  } = CartDataStore.state;

  return (
    <Wrapper>
      <Formik<FormValues>
        key={question}
        initialValues={{
          cashRegisters: rangeToOption<CashRegistersType>(cashRegisters),
          dailyServings: rangeToOption<DailyServingsType>(dailyServings),
          diningPlaces: rangeToOption<DiningPlacesType>(diningPlaces),
          fruitOnMenu: booleanToYesOrNo(fruitOnMenu),
          servingArea: rangeToOption<ServingAreaType>(servingArea),
          vaAcceptance: booleanToYesOrNo(products.va),
          vrAcceptance: booleanToYesOrNo(products.vr),
        }}
        validationSchema={validationBase().shape({
          ...question === 'vr-acceptance' && {
            vrAcceptance: existsValidation(),
          },
          ...question === 'cash-registers' && {
            cashRegisters: existsValidation(),
          },
          ...question === 'daily-servings' && {
            dailyServings: existsValidation(),
          },
          ...question === 'dining-places' && {
            diningPlaces: existsValidation(),
          },
          ...question === 'fruit-on-menu' && {
            fruitOnMenu: existsValidation(),
          },
          ...question === 'serving-area' && {
            servingArea: existsValidation(),
          },
          ...question === 'va-acceptance' && {
            vaAcceptance: existsValidation(),
          },
        })}
        onSubmit={handleFormSubmission}
        validateOnMount
      >
        {({handleSubmit, isValid}) => (
            <Form onSubmit={handleSubmit}>
              {/* Step 01 */}
              {question === 'vr-acceptance' && <VRAcceptance />}
              {/* Step 02 */}
              {question === 'dining-places' && <DiningPlaces />}
              {/* Step 03 */}
              {question === 'daily-servings' && <DailyServings />}
              {/* Step 04 (or 02 if VR is not accepted) */}
              {question === 'serving-area' && <ServingArea />}
              {/* Step 05 */}
              {question === 'fruit-on-menu' && <FruitOnMenu />}
              {/* Step 06 */}
              {question === 'va-acceptance' && <VAAcceptance />}
              {/* Step 07 (or 03 if VR is not accepted) */}
              {question === 'cash-registers' && <CashRegisters />}
              <PQSpacer />
              <SubmitButton
                type='submit'
                isLoading={isLoading}
                disabled={!isValid}
              >
                continuar
              </SubmitButton>
            </Form>
          )}
      </Formik>
      <TermsModal />
      <RequirementsModal />
    </Wrapper>
  );
};

export default observer(PATQuestionsPage);
