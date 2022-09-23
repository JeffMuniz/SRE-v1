
import {FC} from 'react';

import {
  Loading, Text, Wrapper,
} from './loading-button.component.styles';

const LoadingButton: FC<LoadingButton.Props> = ({
  children,
  className,
  disabled,
  id,
  isLoading,
  onClick,
  type,
}) => (
  <Wrapper
    className={className}
    onClick={onClick}
    disabled={disabled || isLoading}
    type={type}
    id={id}
  >
    <Text htmlFor={id} extraMargin={isLoading}>
      {children}
    </Text>
    <Loading show={isLoading} />
  </Wrapper>
);

export default LoadingButton;
