
import {
FC, useCallback, useState,
} from 'react';
import {Formik} from 'formik';
import {observer} from 'mobx-react-lite';
import {useHistory} from 'react-router';
import {ROUTES} from '~/consts';
import {CartDataStore} from '~/stores';
import {nameValidation, validationBase} from '~/utils';
import {NameConfirmationPageStore} from '../name-confirmation.page.store';
import {
 FErrorModal, FSpacer, FallbackInfo, FormEl, Link, NameRadioInput, SubmitButton, Wrapper,
} from './form.styles';

type FormValues = NameConfirmationPage.Form.FormValues;

const Form: FC = () => {
  const [ showErrorModal, setShowErrorModal ] = useState(false);

  const history = useHistory();

  const {name} = CartDataStore.state.selectedOwner;
  const {owners} = CartDataStore.state.establishment;

  const handleFormSubmission = useCallback(async(values: FormValues) => {
    const {cpf} = owners.find(owner => owner.name === values.name);

    await NameConfirmationPageStore.confirmName(values.name, cpf);
    history.push(ROUTES.CPF_CONFIRMATION);
  }, [ owners, history ]);

  const handleErrorModalButtonClick = useCallback((show: boolean) => {
    setShowErrorModal(show);
  }, [ ]);

  return (
    <Wrapper>
      <Formik<FormValues>
        initialValues={{name}}
        onSubmit={handleFormSubmission}
        validateOnMount
        validationSchema={validationBase().shape({
          name: nameValidation(),
        })}
      >
        {({
          handleChange,
          handleSubmit,
          isValid,
          values,
        }) => (
          <FormEl onSubmit={handleSubmit}>
            {owners.map((owner, index) => (
              <NameRadioInput
                checked={values.name === owner.name}
                id={`name-${index}`}
                key={`name-${index}`}
                label={owner.name}
                onChange={handleChange}
                value={owner.name}
              />
            ))}
            <FallbackInfo>
              Se você é sócio mas seu nome não está na lista, <Link onClick={() => handleErrorModalButtonClick(true)}>clique aqui.</Link>
            </FallbackInfo>
            <FSpacer />
            <SubmitButton
              disabled={!isValid}
            >
              confirmar
            </SubmitButton>
          </FormEl>
        )}
      </Formik>
      <FErrorModal
        onCloseButtonClick={() => handleErrorModalButtonClick(false)}
        show={showErrorModal}
        text='É necessário entrar em contato com a central de vendas 4004-4474 ou 0800 723 4474 para atualizar seus dados e continuar o cadastro do seu estabelecimento comercial'
      />
    </Wrapper>
  );
};

export default observer(Form);
