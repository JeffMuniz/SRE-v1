
import {FC} from 'react';
import {useFormikContext} from 'formik';
import {maskMiddleware, phoneMask} from '~/utils';
import {
 CPFInput, CellphoneInput, EmailInput, NameInput, Wrapper,
} from './personal.styles';

type FormValues = DataConfirmationPage.Form.FormValues;

const Personal: FC = () => {

  const {
    errors,
    handleChange,
    touched,
    values,
  } = useFormikContext<FormValues>();

  return (
    <Wrapper>
      <NameInput
        errorMessage={touched.name && errors.name}
        id='name'
        name='name'
        onChange={handleChange}
        value={values.name}
        disabled
      />
      <CPFInput
        errorMessage={touched.cpf && errors.cpf}
        id='cpf'
        name='cpf'
        onChange={handleChange}
        value={values.cpf}
        disabled
      />
      <EmailInput
        errorMessage={touched.email && errors.email}
        id='email'
        name='email'
        onChange={handleChange}
        value={values.email}
      />
      <CellphoneInput
        errorMessage={touched.phone && errors.phone}
        id='phone'
        name='phone'
        onChange={event => maskMiddleware({
          event,
          mask: phoneMask,
          handleChange,
        })}
        placeholder='00 00000-0000'
        value={values.phone}
      />
    </Wrapper>
  );
};

export default Personal;
