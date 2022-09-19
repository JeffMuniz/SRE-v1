
import {FC} from 'react';

import {
 Completed, Indicator, Label, Pending, Wrapper,
} from './progress-bar.component.styles';

const ProgressBar: FC<ProgressBar.Props>  = ({
  className,
  currentStep,
  steps,
}) => {

  if(currentStep < 1) currentStep = 1;
  if(currentStep > steps) currentStep = steps;

  return (
    <Wrapper className={className}>
      <Label>
        {`etapa ${currentStep} de ${steps}`}
      </Label>
      <Indicator>
        <Completed proportion={(currentStep / steps)} />
        <Pending proportion={((steps - currentStep) / steps)} />
      </Indicator>
    </Wrapper>
  );
};

export default ProgressBar;
