
import styled, {css} from 'styled-components';
import {Breadcrumb} from '~/components';
import {mqTablet} from '~/styled';

export const Wrapper = styled.div<{ hide: boolean; }>`
  display: none;

  ${({theme: {colors}, hide}) => css`
    ${mqTablet(css`
      background-color: ${colors.palette.lightGray.light};
      display: ${hide ? 'none' : 'flex'};
      height: 120px;
      justify-content: center;
      padding: 20px;
    `)}
  `}
`;

export const FBreadcrumb = styled(Breadcrumb)``;
