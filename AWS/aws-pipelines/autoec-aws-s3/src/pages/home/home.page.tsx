
import {FC} from 'react';
import Form from './form/form';
import {
 HPageSubtitle, HPageTitle, Wrapper,
} from './home.page.styles';

const HomePage: FC = () => (
  <Wrapper>
    <HPageTitle>
      Em apenas dez minutos, você terá um novo parceiro.
    </HPageTitle>
    <HPageSubtitle>
      digite o seu cnpj
    </HPageSubtitle>
    <Form />
  </Wrapper>
);

export default HomePage;
