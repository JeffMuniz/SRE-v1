
import styled, {css} from 'styled-components';
import {
 BaseLabel, PageSubtitle, PrimaryButton, mqTablet,
} from '~/styled';
import {backgrounder} from '~/styled/helpers/helpers.styled';

const {PUBLIC_URL} = process.env;

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  height: 100vh;
  padding: 30px 20px;
  position: fixed;
  right: 0;
  top: 0;
  width: 100vw;

  ${mqTablet(css`
    ${backgrounder({
      url: `${PUBLIC_URL}/images/promotional-tablet.png`,
    })}
    background-size: 1100px;
    justify-content: center;
  `)}
`;

export const PromotionalImage = styled.img.attrs({
  src: `${PUBLIC_URL}/images/promotional.png`,
})`
  margin-bottom: 20px;
  max-height: 400px;
  max-width: 100%;
  align-self: center;
  ${mqTablet(css`
    display: none;
  `)}
`;

export const TextWrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  ${mqTablet(css`
    align-self: center;
    flex-grow: unset;
    margin-left: 300px;
    width: 400px;
  `)}
`;

export const CPageSubtitle = styled(PageSubtitle)`
  align-self: center;
  margin-bottom: 20px;
  text-align: center;

  ${({theme: {colors}}) => css`
    ${mqTablet(css`
      color: ${colors.palette.pink.main};
      font-size: 30px;
      text-align: left;
    `)}
  `}
`;

export const Info = styled(BaseLabel)`
  align-self: center;
  flex-grow: 1;
  font-size: 12px;
  font-weight: 500;
  margin-bottom: 30px;
  text-align: center;

  ${mqTablet(css`
    flex-grow: unset;
    text-align: left;
  `)}
`;

export const CloseButton = styled(PrimaryButton)`
  ${mqTablet(css`
    align-self: flex-end;
  `)}
`;
