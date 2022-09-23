
import {FC} from 'react';
import {useFormikContext} from 'formik';
import {
 DSPageSubtitle, RadioInput, Wrapper,
} from './daily-servings.styles';

type FormValues = PATQuestionsPage.FormValues;

const DailyServings: FC = () => {

  const {handleChange, values} = useFormikContext<FormValues>();

  return (
    <Wrapper>
      <DSPageSubtitle>
        qual é o número de refeições que serve diariamente?
      </DSPageSubtitle>
      <RadioInput
        checked={values.dailyServings === '0-100'}
        id='daily-servings-1'
        label='até 100'
        name='dailyServings'
        onChange={handleChange}
        value='0-100'
      />
      <RadioInput
        checked={values.dailyServings === '101-200'}
        id='daily-servings-2'
        label='101 - 200'
        name='dailyServings'
        onChange={handleChange}
        value='101-200'
      />
      <RadioInput
        checked={values.dailyServings === '201-299'}
        id='daily-servings-3'
        label='201 - 299'
        name='dailyServings'
        onChange={handleChange}
        value='201-299'
      />
      <RadioInput
        checked={values.dailyServings === '300-or-more'}
        id='daily-servings-4'
        label='300 ou +'
        name='dailyServings'
        onChange={handleChange}
        value='300-or-more'
      />
    </Wrapper>
  );
};

export default DailyServings;
