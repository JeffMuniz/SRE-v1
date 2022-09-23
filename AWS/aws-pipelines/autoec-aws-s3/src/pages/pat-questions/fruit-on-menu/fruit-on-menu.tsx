
import {FC} from 'react';
import {useFormikContext} from 'formik';
import {
 FOMPageSubtitle, RadioInput, Wrapper,
} from './fruit-on-menu.styles';

type FormValues = PATQuestionsPage.FormValues;

const FruitOnMenu: FC = () => {

  const {handleChange, values} = useFormikContext<FormValues>();

  return (
    <Wrapper>
      <FOMPageSubtitle>
        o estabelecimento possui fruta no cardápio?
      </FOMPageSubtitle>
      <RadioInput
        checked={values.fruitOnMenu === 'yes'}
        id='fruit-on-menu-1'
        label='Sim'
        name='fruitOnMenu'
        onChange={handleChange}
        value='yes'
      />
      <RadioInput
        checked={values.fruitOnMenu === 'no'}
        id='fruit-on-menu-2'
        label='Não'
        name='fruitOnMenu'
        onChange={handleChange}
        value='no'
      />
    </Wrapper>
  );
};

export default FruitOnMenu;
