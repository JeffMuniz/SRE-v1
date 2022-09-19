
import {FC} from 'react';
import {useFormikContext} from 'formik';
import {
 CRPageSubtitle, RadioInput, Wrapper,
} from './cash-registers.styles';

type FormValues = PATQuestionsPage.FormValues;

const CashRegisters: FC = () => {

  const {handleChange, values} = useFormikContext<FormValues>();

  return (
    <Wrapper>
      <CRPageSubtitle>
        quantas caixas registradoras o estabelecimento possui?
      </CRPageSubtitle>
      <RadioInput
        checked={values.cashRegisters === '1-30'}
        id='cash-registers-1'
        label='1 - 30'
        name='cashRegisters'
        onChange={handleChange}
        value='1-30'
      />
      <RadioInput
        checked={values.cashRegisters === '31-60'}
        id='cash-registers-2'
        label='31 - 60'
        name='cashRegisters'
        onChange={handleChange}
        value='31-60'
      />
      <RadioInput
        checked={values.cashRegisters === '61-99'}
        id='cash-registers-3'
        label='61 - 99'
        name='cashRegisters'
        onChange={handleChange}
        value='61-99'
      />
      <RadioInput
        checked={values.cashRegisters === '100-or-more'}
        id='cash-registers-4'
        label='100 ou +'
        name='cashRegisters'
        onChange={handleChange}
        value='100-or-more'
      />
    </Wrapper>
  );
};

export default CashRegisters;
