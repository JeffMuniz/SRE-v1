
import {
FC, useCallback, useMemo,
} from 'react';
import {observer} from 'mobx-react-lite';
import {CartDataStore} from '~/stores';
import {
  CardInfo, CardLabel, CardTypeLabel, FoodCard, MealCard, PCheckbox, PatAnswer, PatItem, PatQuestion, PatWrapper, Wrapper,
} from './information.styles';

type Props = DataConfirmationPage.ProductInformation.Props;

const ProductInformation: FC<Props> = ({type}) => {

  const {
    patQuestions: {
      diningPlaces,
      dailyServings,
      servingArea,
      cashRegisters,
    },
  } = CartDataStore.state;

  const getPatAnswerText = useCallback((answer: { min: number; max: number }): string => {
    return `
      ${answer.min ?? 'até'}
      ${(!!!answer.min || !!!answer.max) ? ' ' : ' - '}
      ${answer.max ?? 'ou +'}
    `;
  }, []);

  const patQuestionsText = useMemo(() => {
    return {
      diningPlacesText: getPatAnswerText(diningPlaces),
      dailyServingsText: getPatAnswerText(dailyServings),
      servingAreaText: getPatAnswerText(servingArea),
      cashRegistersText: getPatAnswerText(cashRegisters),
    };
  }, [
    diningPlaces,
    dailyServings,
    servingArea,
    cashRegisters,
    getPatAnswerText,
  ]);

  return (
    <Wrapper>
      <PCheckbox checked onChange={() => { }}>
        {type === 'vr' ? (<MealCard />) : (<FoodCard />)}
      </PCheckbox>
      <CardInfo>
        <CardLabel>cartão</CardLabel>
        <CardTypeLabel>{type === 'vr' ? 'refeição' : 'alimentação'}</CardTypeLabel>
        <PatWrapper>
          {type === 'vr' && (
            <PatItem>
              <PatQuestion>número de lugares: <PatAnswer>{patQuestionsText.diningPlacesText}</PatAnswer></PatQuestion>
            </PatItem>
          )}
          {type === 'vr' && (
            <PatItem>
              <PatQuestion>número de refeições: <PatAnswer>{patQuestionsText.dailyServingsText}</PatAnswer></PatQuestion>
            </PatItem>
          )}
          {!(type === 'vr') && (
            <PatItem>
              <PatQuestion>número de registradoras: <PatAnswer>{patQuestionsText.cashRegistersText}</PatAnswer></PatQuestion>
            </PatItem>
          )}
          <PatItem>
            <PatQuestion>área de atendimento': <PatAnswer>{patQuestionsText.servingAreaText}</PatAnswer></PatQuestion>
          </PatItem>
        </PatWrapper>
      </CardInfo>
    </Wrapper>
  );
};

export default observer(ProductInformation);
