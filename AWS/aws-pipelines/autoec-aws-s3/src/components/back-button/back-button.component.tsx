
import {FC} from 'react';
import {Icon, Wrapper} from './back-button.component.styles';

const BackButton: FC<BackButton.Props> = ({className, onClick}) => (
  <Wrapper className={className} onClick={onClick}>
    <Icon />
  </Wrapper>
);

export default BackButton;
