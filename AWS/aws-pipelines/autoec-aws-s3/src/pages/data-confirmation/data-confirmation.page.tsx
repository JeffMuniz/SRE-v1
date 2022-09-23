import {FC} from 'react';
import {observer} from 'mobx-react-lite';
import {Wrapper} from './data-confirmation.page.styles';
import Form from './form/form';

const DataConfirmationPage: FC = () => {

  return (
    <Wrapper>
      <Form />
    </Wrapper>
  );
};

export default observer(DataConfirmationPage);
