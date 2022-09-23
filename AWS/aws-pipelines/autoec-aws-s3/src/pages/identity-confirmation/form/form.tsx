
import {
FC, useCallback, useEffect,
} from 'react';
import {Formik} from 'formik';
import {observer} from 'mobx-react-lite';
import {useHistory} from 'react-router-dom';
import {PageSpinner} from '~/components';
import {ROUTES} from '~/consts';
import {
 dateStrValidation, nameValidation, toDateString, validationBase,
} from '~/utils';
import {IdentityConfirmationPageStore} from '../identity-confirmation.page.store';
import {
 BirthdayInputWrapper, BirthdayRadioInput, FErrorModal, FPageSubtitle, FSpacer, Form as FormEl, Info, InputWrapper, MotherNameRadioInput, SubmitButton, Wrapper,
} from './form.styles';

type FormValues = IdentityConfirmationPage.Form.FormValues;

const Form: FC = () => {

  const history = useHistory();
  const {showButtonLoading, showPageLoading, options: {motherNames, birthDates}} = IdentityConfirmationPageStore.state;

  useEffect(() => {
    getOwnerInformation();
  }, []);

  const getOwnerInformation = async() => {
    try {
      await IdentityConfirmationPageStore.getOwnerInformation();
    } catch (error) {
      return;
    }
  };

  const handleFormSubmission = async(values: FormValues) => {
    try {
      await IdentityConfirmationPageStore.confirmIdentity(values);
    } catch (error) {
      IdentityConfirmationPageStore.setState(state => {
        state.showErrorModal = true;
      });
      return;
    }

    history.push(ROUTES.ESTABLISHMENT_DATA);
  };

  const handleCloseErrorModalButtonClick = useCallback(() => {
    IdentityConfirmationPageStore.setState(state => {
      state.showErrorModal = false;
    });
  }, [ ]);

  if (showPageLoading) {
    return <PageSpinner />;
  }

  return (
    <Wrapper>
      <Formik<FormValues>
        initialValues={{
          motherName: '',
          birthday: '',
        }}
        validationSchema={validationBase().shape({
          motherName: nameValidation(),
          birthday: dateStrValidation(),
        })}
        onSubmit={handleFormSubmission}
        validateOnMount
      >
        {({
          handleChange,
          handleSubmit,
          values,
          isValid,
        }) => (
          <FormEl onSubmit={handleSubmit}>
            <FPageSubtitle>
              confirme o nome da sua mãe:
            </FPageSubtitle>
            <Info>
              Confirme o nome da sua mãe
            </Info>
            <InputWrapper>
              {motherNames.map((n, index) => (
                <MotherNameRadioInput
                  checked={values.motherName === n}
                  id={`mother-name-${index}`}
                  key={n}
                  label={n}
                  name='motherName'
                  onChange={handleChange}
                  value={n}
                />
              ))}
            </InputWrapper>
            <FPageSubtitle>
              confirme sua data de nascimento:
            </FPageSubtitle>
            <Info>
              Confirme sua data de nascimento
            </Info>
            <BirthdayInputWrapper>
              {birthDates.map((date, index) => {
                const strDate = toDateString({date: new Date(date.split('-').join('/'))});
                return (
                  <BirthdayRadioInput
                    checked={values.birthday === date}
                    id={`birthday-${index}`}
                    key={strDate}
                    label={strDate}
                    name='birthday'
                    onChange={handleChange}
                    value={date}
                  />
                );
              })}
            </BirthdayInputWrapper>
            <FSpacer />
            <SubmitButton type='submit' disabled={!isValid} isLoading={showButtonLoading}>
              continuar
            </SubmitButton>
          </FormEl>
        )}
      </Formik>
      <FErrorModal
        onCloseButtonClick={handleCloseErrorModalButtonClick}
        show={IdentityConfirmationPageStore.state.showErrorModal}
        text={`É necessário entrar em contato com a central de vendas
          4004-4474 ou 0800 723 4474 para atualizar seus dados e continuar
          o cadastro do seu estabelecimento comercial
        `}
      />
    </Wrapper>
  );
};

export default observer(Form);
