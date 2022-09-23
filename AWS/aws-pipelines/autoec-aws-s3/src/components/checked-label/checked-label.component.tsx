
import {FC} from 'react';
import {
 Icon, Label, Wrapper,
} from './checked-label.component.styles';

const CheckedLabel: FC<CheckedLabel.Props> = ({className, label}) => (
  <Wrapper className={className}>
    <Icon />
    <Label>
      {label}
    </Label>
  </Wrapper>
);

export default CheckedLabel;
