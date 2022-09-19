
import {FC} from 'react';
import Form from './form/form';
import {
 Info, NCPageSubtitle, Wrapper,
} from './name-confirmation.page.styles';

const NameConfirmationPage: FC = () => (
  <Wrapper>
    <NCPageSubtitle>
      confirme seu nome:
    </NCPageSubtitle>
    <Info>
      para dar continuidade ao cadastro é necessário que você se identifique
    </Info>
    <Form />
  </Wrapper>
);

export default NameConfirmationPage;
