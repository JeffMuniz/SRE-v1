
import {FC} from 'react';
import Form from './form/form';
import {
 ICPageSubtitle, Info, Wrapper,
} from './identity-confirmation.page.styles';

const IdentityConfirmationPage: FC = () => (
  <Wrapper>
    <ICPageSubtitle>
      confirme alguns dados
    </ICPageSubtitle>
    <Info>
      para garantir sua segurança no credenciamento
    </Info>
    <Form />
  </Wrapper>
);

export default IdentityConfirmationPage;
