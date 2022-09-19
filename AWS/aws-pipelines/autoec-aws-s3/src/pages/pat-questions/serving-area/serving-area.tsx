
import {FC} from 'react';
import {useFormikContext} from 'formik';
import {
  RadioInput, SAPageSubtitle, Wrapper,
} from './serving-area.styles';

type FormValues = PATQuestionsPage.FormValues;

const ServingArea: FC = () => {

  const {handleChange, values} = useFormikContext<FormValues>();

  return (
    <Wrapper>
      <SAPageSubtitle>
        qual é a área de atendimento?
      </SAPageSubtitle>
      <RadioInput
        checked={values.servingArea === '0-50'}
        id='serving-area-1'
        label='até 50m2'
        name='servingArea'
        onChange={handleChange}
        value='0-50'
      />
      <RadioInput
        checked={values.servingArea === '51-100'}
        id='serving-area-2'
        label='51m2 - 100m2'
        name='servingArea'
        onChange={handleChange}
        value='51-100'
      />
      <RadioInput
        checked={values.servingArea === '101-499'}
        id='serving-area-3'
        label='101m2 - 499m2'
        name='servingArea'
        onChange={handleChange}
        value='101-499'
      />
      <RadioInput
        checked={values.servingArea === '500-or-more'}
        id='serving-area-4'
        label='500m2 ou +'
        name='servingArea'
        onChange={handleChange}
        value='500-or-more'
      />
    </Wrapper>
  );
};

export default ServingArea;
