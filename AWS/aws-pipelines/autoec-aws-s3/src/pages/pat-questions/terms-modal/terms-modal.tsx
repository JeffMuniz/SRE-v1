
import {FC, useState} from 'react';
import {Formik} from 'formik';
import {observer} from 'mobx-react-lite';
import {useHistory} from 'react-router-dom';
import {ROUTES} from '~/consts';
import {CartDataStore} from '~/stores';
import {isTrueValidation, validationBase} from '~/utils';
import {PATQuestionsPageStore} from '../pat-questions.page.store';
import {
 CheckboxesWrapper, ContinueButton, Form, TMCheckbox, TMPageSubtitle, Wrapper,
} from './terms-modal.styles';

type State = PATQuestionsPage.TermsModal.State;
type FormValues = PATQuestionsPage.TermsModal.FormValues;

const TermsModal: FC = () => {

  const history = useHistory();

  const [ state, setState ] = useState<State>({
    isLoading: false,
  });

  const handleBackgroundClick = () => {
    PATQuestionsPageStore.setState(state => {
      state.showTermsModal = false;
    });
  };

  const handleFormSubmission = async(values: FormValues) => {
    setState(prev => ({...prev, isLoading: true}));
    CartDataStore.setState(cartState => {
      cartState.termsAcceptance.nutritional = true;
      cartState.termsAcceptance.truthfulInfo = true;
    });

    try {
      await CartDataStore.update();
    } catch(error) {
      console.error(error);
      return;
    } finally {
      setState(prev => ({...prev, isLoading: false}));
      history.push(ROUTES.ACQUIRERS);
    }
  };

  const {products} = CartDataStore.state;
  const {showTermsModal} = PATQuestionsPageStore.state;

  return (
    <Wrapper
      show={showTermsModal}
      onBackgroundClick={handleBackgroundClick}
    >
      <TMPageSubtitle>
        marque os campos para prosseguir
      </TMPageSubtitle>
      <Formik<FormValues>
        initialValues={{
          nutritionalTerm: false,
          truthfulInfoTerm: false,
        }}
        validationSchema={validationBase().shape({
          ...products.vr && {
            nutritionalTerm: isTrueValidation(),
          },
          ...products.va && {
            truthfulInfoTerm: isTrueValidation(),
          },
        })}
        onSubmit={handleFormSubmission}
        validateOnMount
      >
        {({handleSubmit, handleChange, isValid}) => (
          <Form onSubmit={handleSubmit}>
            <CheckboxesWrapper>
              {products.vr && (
                <TMCheckbox
                  id='nutritional-term'
                  label='Estou ciente de que as refeições em meu estabelecimento devem atender as exigências nutricionais estabelecidas, incluindo frutas, bem como oferecer sugestão diária de cardápio saudável em local de fácil consulta, nos termos da legislação do Programa de Alimentação do Trabalhador ("PAT")'
                  name='nutritionalTerm'
                  onChange={handleChange}
                />
              )}
              {products.va && (
                <TMCheckbox
                  id='truthful-info-term'
                  label='Declaro que as informações acima são verdadeiras e aderente ao programa PAT previsto em lei vigente.'
                  name='truthfulInfoTerm'
                  onChange={handleChange}
                />
              )}
            </CheckboxesWrapper>
            <ContinueButton isLoading={state.isLoading} disabled={!isValid}>
              continuar
            </ContinueButton>
          </Form>
        )}
      </Formik>
    </Wrapper>
  );
};

export default observer(TermsModal);
