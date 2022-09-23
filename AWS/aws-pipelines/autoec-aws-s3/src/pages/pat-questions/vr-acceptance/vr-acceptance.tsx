
import {FC} from 'react';
import {useFormikContext} from 'formik';
import {
 CardImage, InlineWrapper, RadioInput, RadioWrapper, VRAPageSubtitle, Wrapper,
} from './vr-acceptance.styles';

type FormValues = PATQuestionsPage.FormValues;

const VRAcceptance: FC = () => {

  const {handleChange, values} = useFormikContext<FormValues>();

  return (
    <Wrapper>
      <VRAPageSubtitle>
        seu estabelecimento aceita cartão refeição?
      </VRAPageSubtitle>
      <InlineWrapper>
        <CardImage />
        <RadioWrapper>
          <RadioInput
            checked={values.vrAcceptance === 'yes'}
            id='vr-acceptance-1'
            label='Sim'
            name='vrAcceptance'
            onChange={handleChange}
            value='yes'
          />
          <RadioInput
            checked={values.vrAcceptance === 'no'}
            id='vr-acceptance-2'
            label='Não'
            name='vrAcceptance'
            onChange={handleChange}
            value='no'
          />
        </RadioWrapper>
      </InlineWrapper>
    </Wrapper>
  );
};

export default VRAcceptance;
