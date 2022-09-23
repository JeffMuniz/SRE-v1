
import {
FC, useCallback, useState,
} from 'react';
import {Formik} from 'formik';
import {observer} from 'mobx-react-lite';
import {useHistory} from 'react-router';
import {ROUTES} from '~/consts';
import {CartDataStore} from '~/stores';
import {
 cnpjMask, cnpjValidation, maskMiddleware, validationBase,
} from '~/utils';
import {HomePageStore} from '../home.page.store';
import {Form as FormEl, Wrapper} from './form.styles';
import {
 CNPJInput, ContinueButton, FErrorModal, FSpacer,
} from './form.styles';

type FormValues = HomePage.Form.FormValues;
type State = HomePage.Form.State;

const {REACT_APP_EC_LOGIN_URL} = process.env;

const Form: FC = () => {
  const [ state, setState ] = useState<State>({
    title: '',
    text: '',
    show: false,
    buttonText: '',
    cnpjStatus: '',
  });

  const history = useHistory();

  const handleSubmit = async(values: FormValues) => {
    try {
      await HomePageStore.getCnpjStatus(values.cnpj);
      /* const cnpjStatus = await HomePageStore.getCnpjStatus(values.cnpj);
      if (cnpjStatus === 'BLOCKED') {
        setState(prev => ({
          ...prev,
          title: 'Este CNPJ já passa mac!',
          text: `O CNPJ que você inseriu já está cadastrado em nossa base.${'\n'}
            Acesse a plataforma disponível para você, clique no botão para fazer login`,
          show: true,
          buttonText: 'Ir para o login',
          cnpjStatus,
        }));
        return;
      }
      if (cnpjStatus === 'ANALYZING') {
        setState(prev => ({
          ...prev,
          title: 'Seu cadastro está em análise...',
          text: `Um pedido de cadastro já foi realizado para este CNPJ,
            estamos analisando todas as informações e enviaremos um email com a
            resposta no máximo em 5 dias úteis.`,
          show: true,
          buttonText: 'Fechar',
          cnpjStatus,
        }));
        return;
      } */
    } catch (error) {
      setState(prev => ({
        ...prev,
        show: true,
        text: `É necessário entrar em contato com a central de vendas
          4004-4474 ou 0800 723 4474 para atualizar seus dados e continuar
          o cadastro do seu estabelecimento comercial`,
        buttonText: 'Fechar',
        cnpjStatus: '',
      }));
      return;
    }

    history.push(ROUTES.NAME_CONFIRMATION);
  };

  const handleCloseErrorModalButtonClick = useCallback(() => {
    if (state.cnpjStatus === 'BLOCKED') window.open(REACT_APP_EC_LOGIN_URL, '_blank');

    setState(prev => ({
      ...prev,
      show: false,
      text: '',
      title: '',
    }));
  }, [ state ]);

  const {isLoading} = HomePageStore.state;
  const {establishment: {cnpj}} = CartDataStore.state;

  return (
    <Wrapper>
      <Formik<FormValues>
        initialValues={{cnpj}}
        onSubmit={handleSubmit}
        validationSchema={validationBase().shape({
          cnpj: cnpjValidation(),
        })}
      >
        {({
            errors,
            handleBlur,
            handleChange,
            handleSubmit,
            isValid,
            touched,
            values,
          }) => (
            <FormEl onSubmit={handleSubmit}>
              <CNPJInput
                autoFocus
                onChange={event => maskMiddleware({
                  event,
                  mask: cnpjMask,
                  handleChange,
                })}
                onBlur={handleBlur}
                errorMessage={touched.cnpj && errors.cnpj}
                value={values.cnpj}
              />
              <FSpacer />
              <ContinueButton
                isLoading={isLoading}
                disabled={!isValid}
              >
                continuar
              </ContinueButton>
            </FormEl>
          )}
      </Formik>
      <FErrorModal
        onCloseButtonClick={handleCloseErrorModalButtonClick}
        show={state.show}
        text={state.text}
        title={state.title}
        buttonText={state.buttonText}
      />
    </Wrapper>
  );
};

export default observer(Form);
