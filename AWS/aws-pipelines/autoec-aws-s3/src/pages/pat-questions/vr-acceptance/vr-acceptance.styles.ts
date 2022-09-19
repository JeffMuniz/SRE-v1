
import styled from 'styled-components';
import {Radio} from '~/components';
import {PageSubtitle} from '~/styled';

const {PUBLIC_URL} = process.env;

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

export const VRAPageSubtitle = styled(PageSubtitle)`
  margin-bottom: 20px;
`;

export const InlineWrapper = styled.div`
  display: flex;
`;

export const RadioWrapper = styled.div`
  align-self: center;
`;

export const RadioInput = styled(Radio)`
  margin-bottom: 15px;

  &:last-child {
    margin-bottom: 0;
  }
`;

export const CardImage = styled.img.attrs({
  src: `${PUBLIC_URL}/images/meal-card.png`,
})`
  align-self: center;
  margin-right: 10px;
  margin-left: -10px;
`;
