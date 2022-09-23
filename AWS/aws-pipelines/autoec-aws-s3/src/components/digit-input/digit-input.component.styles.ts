
import styled, {css} from 'styled-components';
import {BaseInput, BaseLabel} from '~/styled';

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

export const Label = styled(BaseLabel)`
  font-size: 14px;
  margin-bottom: 10px;

  ${({theme: {colors}}) => css`
    color: ${colors.text.title};
  `}
`;

export const InputWrapper = styled.div`
  display: flex;
`;

export const Input = styled(BaseInput).attrs({
  maxLength: 1,
})`
  width: 45px;
  border-radius: 8px;
  border: none;
  margin-right: 10px;
  text-align: center;

  &:last-child {
    margin-right: 0;
  }
  
  ${({theme: {colors}}) => css`
    border: 2px solid ${colors.palette.lightGray.main};
  `}
`;

// export const HiddenInput = styled.input`
//   max-height: 0;
//   max-width: 0;
//   opacity: 0;
// `;
