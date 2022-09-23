
import {FC} from 'react';
import {CPPageSubtitle, Wrapper} from './contact-preference.page.styles';
import Form from './form/form';

const ContactPreferencePage: FC = () => (
  <Wrapper>
    <CPPageSubtitle>
      por onde você gostaria que enviássemos o código de confirmação?
    </CPPageSubtitle>
    <Form />
  </Wrapper>
);

export default ContactPreferencePage;
