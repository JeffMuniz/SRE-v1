
import {FC} from 'react';
import {useFormikContext} from 'formik';
import {
 DPPageSubtitle, RadioInput, Wrapper,
} from './dining-places.styles';

type FormValues = PATQuestionsPage.FormValues;

const DiningPlaces: FC = () => {

  const {handleChange, values} = useFormikContext<FormValues>();

  return (
    <Wrapper>
      <DPPageSubtitle>
        qual é o número de lugares do seu estabelecimento?
      </DPPageSubtitle>
      <RadioInput
        checked={values.diningPlaces === '1-30'}
        id='dining-places-1'
        label='1 - 30'
        name='diningPlaces'
        onChange={handleChange}
        value='1-30'
      />
      <RadioInput
        checked={values.diningPlaces === '31-60'}
        id='dining-places-2'
        label='31 - 60'
        name='diningPlaces'
        onChange={handleChange}
        value='31-60'
      />
      <RadioInput
        checked={values.diningPlaces === '61-99'}
        id='dining-places-3'
        label='61 - 99'
        name='diningPlaces'
        onChange={handleChange}
        value='61-99'
      />
      <RadioInput
        checked={values.diningPlaces === '100-or-more'}
        id='dining-places-4'
        label='100 ou +'
        name='diningPlaces'
        onChange={handleChange}
        value='100-or-more'
      />
    </Wrapper>
  );
};

export default DiningPlaces;
