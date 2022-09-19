
import {FC} from 'react';
import {CCPageSubtitle, Wrapper} from './confirmation-code.page.styles';
import Form from './form/form';

const ConfirmationCodePage: FC = () => (
  <Wrapper>
    <CCPageSubtitle>
      insira o código
    </CCPageSubtitle>
    <Form />
  </Wrapper>
);

export default ConfirmationCodePage;
