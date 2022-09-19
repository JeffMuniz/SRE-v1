
import {FC} from 'react';
import {
 CCPageSubtitle, Info, Wrapper,
} from './cpf-confirmation.page.styles';
import Form from './form/form';

const CPFConfirmationPage: FC = () => (
  <Wrapper>
    <CCPageSubtitle>
      confirme seus dados:
    </CCPageSubtitle>
    <Info>
      você deve ser o responsável legal. confirme seu nome e seu cpf.
    </Info>
    <Form />
  </Wrapper>
);

export default CPFConfirmationPage;
