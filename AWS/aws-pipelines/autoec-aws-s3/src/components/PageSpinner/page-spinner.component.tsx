
import {FC} from 'react';
import {Loading, Wrapper} from './page-spinner.component.styles';

const PageSpinner: FC = () => (
  <Wrapper show>
    <Loading />
  </Wrapper>
);

export default PageSpinner;
