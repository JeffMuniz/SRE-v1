
import {FC} from 'react';
import {EDPageSubtitle, Wrapper} from './establishment-data.page.styles';
import Form from './form/form';

const EstablishmentData: FC = () => (
  <Wrapper>
    <EDPageSubtitle>
      confirme os dados do seu estabelecimento:
    </EDPageSubtitle>
    <Form />
  </Wrapper>
);

export default EstablishmentData;
