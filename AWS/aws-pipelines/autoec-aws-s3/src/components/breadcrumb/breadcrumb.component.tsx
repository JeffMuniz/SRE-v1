
import {FC, Fragment} from 'react';
import {
 Circle, Label, Line, StepWrapper, Wrapper,
} from './breadcrumb.component.styles';

const Breadcrumb: FC<Breadcrumb.Props> = ({steps}) => (
  <Wrapper>
    {steps.map((s, index) => (
      <Fragment key={index}>
        <StepWrapper key={`${s.label}-${index}`}>
          <Label>
            {s.label}
          </Label>
          <Circle highlight={s.isActive} />
        </StepWrapper>
        {index < steps.length - 1 && (
          <Line />
        )}
      </Fragment>
    ))}
  </Wrapper>
);

export default Breadcrumb;
