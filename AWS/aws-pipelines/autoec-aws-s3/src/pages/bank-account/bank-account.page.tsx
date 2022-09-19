
import {FC} from 'react';
import {
 BAPageSubtitle, Info, Wrapper,
} from './bank-account.page.styles';
import Form from './form/form';

const BankAccountPage: FC = () => {

  return (
    <Wrapper>
      <BAPageSubtitle>
        informe sua conta bancária jurídica para recebimento
      </BAPageSubtitle>
      <Info>
        essa conta bancária deve ser do CNPJ abaixo 00.000.000/0000-00
      </Info>
      <Form />
    </Wrapper>
  );
};

export default BankAccountPage;
