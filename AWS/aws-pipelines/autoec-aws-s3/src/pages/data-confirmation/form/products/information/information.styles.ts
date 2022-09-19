
import styled, {css} from 'styled-components';
import {Checkbox} from '~/components';
import {
 BaseLabel, Clickable, mqTablet,
} from '~/styled';

const {PUBLIC_URL} = process.env;

export const EditLabel = styled(Clickable)``;

export const TabletInlineWrapper = styled.div`
  margin-top: 20px;
  ${mqTablet(css`
    display: flex;
    flex-wrap: wrap; 
  `)}
`;

export const PCheckbox = styled(Checkbox)`
  * {
    align-self: center;
  }
  & > div {
    margin-right: 5px;
  }
`;

export const MealCard = styled.img.attrs({
  src: `${PUBLIC_URL}/images/meal-card.png`,
})`
  align-self: center;
  display: block;
  height: 154px;
`;

export const FoodCard = styled(MealCard).attrs({
  src: `${PUBLIC_URL}/images/food-card.png`,
})`
`;

export const CardInfo = styled.div`
  flex-direction: column;
`;

const DefaultLabel = styled(BaseLabel)`
  ${({theme: {colors}}) => css`
    color: ${colors.palette.black.main};
  `}
`;

export const CardLabel = styled(DefaultLabel)`
  display: block;
  font-size: 18px;
`;

export const CardTypeLabel = styled(DefaultLabel)`
  font-size: 22px;
  font-weight: 600;
`;

export const PatWrapper = styled.div`
  margin-top: 12px;
`;

export const PatItem = styled.div`
  display: block;
`;

export const PatQuestion = styled(DefaultLabel)`
  font-size: 15px;
`;

export const PatAnswer = styled(PatQuestion)`
  font-weight: 600;
`;

export const Wrapper = styled.div`
  display: flex;
  flex-direction: row;
  margin-top: 16px;

  ${mqTablet(css`
    margin-right: 24px;
  `)}
`;
