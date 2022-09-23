
import {FlattenSimpleInterpolation, css} from 'styled-components';

const mq = ({cssBlock, minWidth}: {
  cssBlock: FlattenSimpleInterpolation;
  minWidth: number;
}) => css`
  @media (min-width: ${minWidth}px) {
    ${cssBlock}
  }
`;

export const mqTablet = (cssBlock: FlattenSimpleInterpolation) => mq({
  cssBlock,
  minWidth: 768,
});

export const mqDesktop = (cssBlock: FlattenSimpleInterpolation) => mq({
  cssBlock,
  minWidth: 1080,
});
