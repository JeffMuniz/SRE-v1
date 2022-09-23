
import {FC} from 'react';
import {
  CheckMark,
  HiddenCheckbox,
 InnerSquare, Label, Square,
 Wrapper,
} from './checkbox.component.styles';

const Checkbox: FC<Checkbox.Props> = ({
  checked,
  children,
  className,
  id,
  label,
  name,
  onChange,
}) => (
  <Wrapper className={className}>
    <HiddenCheckbox
      id={id}
      name={name}
      onChange={onChange}
      checked={checked}
    />
    <Square>
      <InnerSquare>
        <CheckMark />
      </InnerSquare>
    </Square>
      {children || (
        <Label htmlFor={id}>
          {label}
        </Label>
      )}
  </Wrapper>
);

export default Checkbox;
