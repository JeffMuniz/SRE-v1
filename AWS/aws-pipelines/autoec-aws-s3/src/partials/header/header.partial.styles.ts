
import styled, {css} from 'styled-components';
import {BackButton, ProgressBar} from '~/components';
import {PageSubtitle, mqTablet} from '~/styled';

export const Wrapper = styled.div<{
  hide: boolean;
  hideTablet: boolean;
}>`
  height: 60px;
  padding: 20px;
  padding-bottom: 0;
  position: fixed;
  width: 100%;
  
  ${({theme: {colors}}) => css`
    background-color: ${colors.palette.white.main};
  `}

  ${({hide}) => hide && css`
    display: none;
  `}

  ${mqTablet(css`
    display: flex;
    height: 80px;
    justify-content: center;
  `)}

  ${({theme: {colors}}) => css`
    ${mqTablet(css`
      border-bottom: 1px solid ${colors.text.common};
    `)}
  `}

  ${({hideTablet}) => hideTablet && css`
    ${mqTablet(css`
      display: none;
    `)}
  `}
`;

export const InnerWrapper = styled.div`
  height: 100%;
  width: 100%;

  ${mqTablet(css`
    display: flex;
    max-width: 900px;
  `)}
`;

export const HBackButton = styled(BackButton)`
  width: 20px;

  ${mqTablet(css`
    align-self: center;
    margin-right: 20px;
  `)}
`;

export const HPageSubtitle = styled(PageSubtitle)`
  display: none;
  ${mqTablet(css`
    align-self: center;
    display: block;
    font-size: 20px;
    line-height: 20px;
  `)}
`;

export const HProgressBar = styled(ProgressBar)`
  margin-top: -25px;

  ${mqTablet(css`
    display: none;
  `)}
`;
