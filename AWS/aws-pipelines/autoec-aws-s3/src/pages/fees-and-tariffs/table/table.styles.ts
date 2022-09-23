import styled, {css} from 'styled-components';
import {
ErrorModal, PageSpinner, Table,
} from '~/components';
import {BaseLabel, mqTablet} from '~/styled';

export const Wrapper = styled(Table)`
  margin-bottom: 50px;
`;

export const Text = styled(BaseLabel)`
  display: block;
  font-size: 14px;
  font-weight: 500;
  text-align: center;

  ${({theme: {colors}}) => css`
    color: ${colors.palette.black.main};
  `}

  ${mqTablet(css`
    text-align: left;
  `)}
`;

export const ColumnTitle = styled(Text)`
  font-size: 18px;
  font-weight: 700;
`;

export const TextBold = styled(Text)`
  font-weight: 700;
  ${({theme: {colors}}) => css`
    color: ${colors.palette.black.main};
  `}
`;

export const MPageSpinner = styled(PageSpinner)``;

export const MErrorModal = styled(ErrorModal)``;
