
import {
FC, useCallback, useState,
} from 'react';
import {Formik} from 'formik';
import {useHistory} from 'react-router';
import {ROUTES} from '~/consts';
import {postSendConfirmationCode} from '~/services/confirmation-code/confirmation-code.service';
import {CartDataStore} from '~/stores';
import {
customValidation, onlyNumbersMask, validationBase,
} from '~/utils';
import {ConfirmationCodePageStore} from '../confirmation-code.page.store';
import {
 FDigitInput, FErrorModal, FSpacer, Form as FormEl, InvalidCodeLabel, ResendLabel, SubmitButton, Wrapper,
} from './form.styles';

type FormValues = ConfirmationCodePage.Form.FormValues;
type State = ConfirmationCodePage.Form.State;

const Form: FC = () => {
  const [ state, setState ] = useState<State>({
    showErrorModal: false,
    showErrorText: false,
  });

  const {isLoading} = ConfirmationCodePageStore.state;

  const history = useHistory();

  const handleFormSubmission = async(values: FormValues) => {
    if (state.showErrorText) setState(prev => ({
      ...prev,
      showErrorText: false,
    }));

    try {
      await ConfirmationCodePageStore.confirmCode(values.code);
    } catch(error) {
      setState(prev => ({
        ...prev,
        showErrorText: true,
      }));
      return;
    }

    history.push(ROUTES.IDENTITY_CONFIRMATION);
  };

  const resendCode = async() => {
    const {selectedOwner: {email, phone}, contactType} = CartDataStore.state;

    try {
      await postSendConfirmationCode({
        contactType,
        resend: true,
        email,
        phone: onlyNumbersMask(phone),
      });
    } catch(error) {
      setState(prev => ({
        ...prev,
        showErrorModal: true,
      }));
      return;
    }

    setState(prev => ({
      ...prev,
      showErrorText: false,
    }));
  };

  const handleCloseErrorModalButtonClick = useCallback(() => {
    setState(prev => ({
      ...prev,
      showErrorModal: false,
    }));
  }, [ ]);

  return (
    <Wrapper>
      <Formik<FormValues>
        initialValues={{code: ''}}
        validationSchema={validationBase().shape({
          code: customValidation()
            .string()
            .length(6)
            .required(),
        })}
        initialTouched={{code: true}}
        onSubmit={handleFormSubmission}
        validateOnMount
      >
        {({
          handleSubmit,
          setFieldValue,
          isValid,
        }) => (
          <FormEl onSubmit={handleSubmit}>
            <FDigitInput
              autoFocus
              onChange={(value) => setFieldValue('code', value)}
            />
            {state.showErrorText && <InvalidCodeLabel>Código inválido</InvalidCodeLabel>}
            <ResendLabel onClick={resendCode}>
              reenviar código
            </ResendLabel>
            <FSpacer />
            <SubmitButton type='submit' disabled={!isValid} isLoading={isLoading}>
              continuar
            </SubmitButton>
          </FormEl>
        )}
      </Formik>
      <FErrorModal
        onCloseButtonClick={handleCloseErrorModalButtonClick}
        show={state.showErrorModal}
        text='É necessário entrar em contato com a central de vendas 4004-4474 ou 0800 723 4474 para atualizar seus dados e continuar o cadastro do seu estabelecimento comercial'
      />
    </Wrapper>
  );
};

export default Form;
