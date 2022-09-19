
import styled, {css} from 'styled-components';
import {
 BaseLabel, pointer, scrollingShadow,
} from '~/styled';

export const Wrapper = styled.div<{show: boolean;}>`
  ${scrollingShadow}
  border-radius: 5px;
  box-shadow: 0px 5px 10px 1px rgba(0,0,0,0.2);
  display: flex;
  flex-direction: column;
  margin-top: 5px;
  max-height: 0;
  opacity: 0;
  overflow: hidden;
  overflow-y: auto;
  position: relative;
  transition: ease .3s;

  ${({show}) => show && css`
    max-height: 120px;
    opacity: 1;
  `}
`;

export const Label = styled(BaseLabel)<{ highlight: boolean; }>`
  line-height: 20px;
  padding: 10px;
  ${pointer}

  ${({highlight, theme: {colors}}) => highlight && css`
    background-color: ${colors.palette.lightGray.light};
  `}
`;
