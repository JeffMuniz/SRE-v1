
import {
FC, useCallback, useState,
} from 'react';
import {Formik} from 'formik';
import {observer} from 'mobx-react-lite';
import {useHistory} from 'react-router';
import {ROUTES} from '~/consts';
import {CartDataStore} from '~/stores';
import {
 emailValidation, maskMiddleware, phoneMask, phoneValidation, validationBase,
} from '~/utils';
import {ContactInfoPageStore} from '../contact-info.page.store';
import {
 EmailInput, FErrorModal, FSpacer, Form as FormEl, PhoneInput, SubmitButton, Wrapper,
} from './form.styles';

type FormValues = ContactInfoPage.Form.FormValues;
type State = ContactInfoPage.Form.State;

const Form: FC = () => {
  const history = useHistory();
  const {isLoading} = ContactInfoPageStore.state;
  const [ state, setState ] = useState<State>({
    showModal: false,
  });

  const handleFormSubmission = async(values: FormValues) => {
    try {
      await ContactInfoPageStore.submitData(values);
    } catch (error) {
      setState(prev => ({...prev, showModal: true}));
      return;
    }

    history.push(ROUTES.CONFIRMATION_CODE);
  };

  const handleCloseErrorModalButtonClick = useCallback(() => {
    setState(prev => ({...prev, showModal: false}));
  }, [ ]);

  const {email, phone} = CartDataStore.state.selectedOwner;

  return (
    <Wrapper>
      <Formik<FormValues>
        initialValues={{email, phone}}
        validationSchema={validationBase().shape({
          email: emailValidation(),
          phone: phoneValidation(),
        })}
        onSubmit={handleFormSubmission}
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
            <EmailInput
              autoFocus
              errorMessage={touched.email && errors.email}
              id='email'
              label='Email:'
              name='email'
              onBlur={handleBlur}
              onChange={handleChange}
              placeholder='exemplo@gmail.com'
              value={values.email}
            />
            <PhoneInput
              errorMessage={touched.phone && errors.phone}
              id='phone'
              label='Telefone:'
              name='phone'
              onBlur={handleBlur}
              onChange={event => maskMiddleware({
                event,
                mask: phoneMask,
                handleChange,
              })}
              placeholder='00 000000000'
              value={values.phone}
            />
            <FSpacer />
            <SubmitButton disabled={!isValid} isLoading={isLoading}>
              continuar
            </SubmitButton>
          </FormEl>
        )}
      </Formik>
      <FErrorModal
        onCloseButtonClick={handleCloseErrorModalButtonClick}
        show={state.showModal}
        text='É necessário entrar em contato com a central de vendas 4004-4474 ou 0800 723 4474 para atualizar seus dados e continuar o cadastro do seu estabelecimento comercial'
      />
    </Wrapper>
  );
};

export default observer(Form);
