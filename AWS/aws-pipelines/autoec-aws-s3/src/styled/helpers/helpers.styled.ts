
import {parseToRgb, rgba} from 'polished';
import SVG from 'react-inlinesvg';
import styled, {css} from 'styled-components';

export const backgrounder = ({url}: {
  url: string;
}) => css`
  background-image: url('${url}');
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
`;

export const pointer = () => css`
  &, * {
    cursor: pointer;
  }
`;

export const dim = ({
  amount = 0.7,
  color,
}: {
  amount?: number;
  color?: string;
} = {
  amount: 0.7,
}) => css`
  ${({theme: {colors}}) => css`
    background-color: ${rgba({
      ...parseToRgb(color || colors.palette.black.main),
      alpha: amount,
      })};
  `}
`;

export const noScrollbar = () => css`
  &::-webkit-scrollbar {
    display: none;
  }
`;

export const spin = ({
  animationName,
  duration = 1,
  iterations = 'infinite',
  timingFunction = 'linear',
}: {
  animationName: string;
  duration?: number;
  iterations?: 'infinite' | number;
  timingFunction?: 'linear' | 'ease'; // Add as you please.
}) => css`
  @keyframes ${animationName} {
    from {
      transform: rotate(0deg);
    }
    to {
      transform: rotate(360deg);
    }
  }

  animation-duration: ${duration}s;
  animation-iteration-count: ${iterations};
  animation-name: ${animationName};
  animation-timing-function: ${timingFunction};
`;

export const scrollingShadow = () => css`
  background: linear-gradient(white 30%, rgba(255, 255, 255, 0)),
              linear-gradient(rgba(255, 255, 255, 0), white 70%) 0 100%,
              radial-gradient(farthest-side at 50% 0,
              rgba(0, 0, 0, 0.07),
              rgba(0, 0, 0, 0)),
              radial-gradient(farthest-side at 50% 100%,
              rgba(0, 0, 0, 0.07),
              rgba(0, 0, 0, 0)) 0 100%;
  background-attachment: local, local, scroll, scroll;
  background-color: white;
  background-repeat: no-repeat;
  /* Opera doesn't support this in the shorthand */
  background-size: 100% 40px, 100% 40px, 100% 14px, 100% 14px;
  transition: height 0.3s ease-in 0.3s;
`;

export const Spacer = styled.div`
  flex-grow: 1;
`;

export const SVGImporter = styled(SVG)`
  ${({theme: {colors}}) => css`
    path {
      fill: ${colors.palette.black.main};
    }
  `}
`;
