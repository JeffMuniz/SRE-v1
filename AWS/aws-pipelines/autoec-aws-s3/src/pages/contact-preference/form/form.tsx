
import {
FC, useCallback, useState,
} from 'react';
import {Formik} from 'formik';
import {observer} from 'mobx-react-lite';
import {useHistory} from 'react-router';
import {ROUTES} from '~/consts';
import {existsValidation, validationBase} from '~/utils';
import {ContactPreferencePageStore} from '../contact-preference.page.store';
import {
 FErrorModal, FSpacer, Form as FormEl, PreferredMediumInput, SubmitButton, Wrapper,
} from './form.styles';

type FormValues = ContactPreferencePage.Form.FormValues;

const Form: FC = () => {
  const [ showErrorModal, setShowErrorModal ] = useState(false);

  const history = useHistory();

  const handleFormSubmission = async(values: FormValues) => {
    try {
      await ContactPreferencePageStore.sendCode(values.preferredMedium);
    } catch (error) {
      setShowErrorModal(true);
      return;
    }

    history.push(ROUTES.CONFIRMATION_CODE);
  };

  const {isLoading} = ContactPreferencePageStore.state;

  const handleCloseErrorModalButtonClick = useCallback(() => {
    setShowErrorModal(false);
  }, [ ]);

  return (
    <Wrapper>
      <Formik<FormValues>
        onSubmit={handleFormSubmission}
        initialValues={{
          preferredMedium: null,
        }}
        validationSchema={validationBase().shape({
          preferredMedium: existsValidation(),
        })}
        validateOnMount
      >
        {({
          handleChange,
          handleSubmit,
          isValid,
          values,
        }) => (
          <FormEl onSubmit={handleSubmit}>
            {[{
              value: 'phone',
              label: 'celular',
            }, {
              value: 'email',
              label: 'email',
            }].map((m, index) => (
              <PreferredMediumInput
                key={`preferred-medium-${index}`}
                label={m.label}
                id={`preferred-medium-${index}`}
                name='preferredMedium'
                onChange={handleChange}
                value={m.value}
                checked={values.preferredMedium === m.value}
              />
            ))}
            <FSpacer />
            <SubmitButton isLoading={isLoading} disabled={!isValid}>
              continuar
            </SubmitButton>
          </FormEl>
        )}
      </Formik>
      <FErrorModal
        onCloseButtonClick={handleCloseErrorModalButtonClick}
        show={showErrorModal}
        text='É necessário entrar em contato com a central de vendas 4004-4474 ou 0800 723 4474 para atualizar seus dados e continuar o cadastro do seu estabelecimento comercial'
      />
    </Wrapper>
  );
};

export default observer(Form);
