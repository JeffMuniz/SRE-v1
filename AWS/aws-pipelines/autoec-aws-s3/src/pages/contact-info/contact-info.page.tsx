
import {FC} from 'react';
import {
 CIPageSubtitle, Info, Wrapper,
} from './contact-info.page.styles';
import Form from './form/form';

const ContactInfoPage: FC = () => (
  <Wrapper>
    <CIPageSubtitle>
      informe os seus dados:
    </CIPageSubtitle>
    <Info>
      você deve ser o responsável legal.
    </Info>
    <Form />
  </Wrapper>
);

export default ContactInfoPage;
