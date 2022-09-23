
import {fontFace} from 'polished';
import {FlattenSimpleInterpolation, css} from 'styled-components';

const {PUBLIC_URL} = process.env;

const setupBuilder = ({
  family,
  fileFormats,
  basePath,
  style = 'regular',
}: {
  family: string;
  fileFormats: Array<string>;
  basePath: string;
  style?: 'regular' | 'italic',
}) => {

  const builder = ({
    weight,
    fileName,
    fileFormats: builderFileFormats,
  }: {
    weight: string;
    fileName: string;
    fileFormats?: Array<string>;
  }) => fontFace({
    fileFormats: builderFileFormats || fileFormats,
    fontFamily: family,
    fontFilePath: `${PUBLIC_URL}/${basePath}/${fileName}`,
    fontStyle: style,
    fontWeight: weight,
  });

  return builder;
};

const openSans = (): FlattenSimpleInterpolation => {

  const baseParams = {
    basePath: 'fonts/open-sans',
    family: 'Open Sans',
    fileFormats: [ 'ttf' ],
  };

  const build = setupBuilder({...baseParams});
  const buildItalic = setupBuilder({...baseParams, style: 'italic'});

  return css`
    ${build({fileName: 'OpenSans-Bold', weight: 'bold'})};
    ${build({fileName: 'OpenSans-Regular', weight: 'normal'})};

    ${build({fileName: 'OpenSans-ExtraBold', weight: '800'})};
    ${build({fileName: 'OpenSans-Bold', weight: '700'})};
    ${build({fileName: 'OpenSans-SemiBold', weight: '600'})};
    ${build({fileName: 'OpenSans-Regular', weight: '400'})};
    ${build({fileName: 'OpenSans-Light', weight: '300'})};

    ${buildItalic({fileName: 'OpenSans-BoldItalic', weight: 'bold'})};
    ${buildItalic({fileName: 'OpenSans-Italic', weight: 'normal'})};

    ${buildItalic({fileName: 'OpenSans-ExtraBoldItalic', weight: '800'})};
    ${buildItalic({fileName: 'OpenSans-BoldItalic', weight: '700'})};
    ${buildItalic({fileName: 'OpenSans-SemiBoldItalic', weight: '600'})};
    ${buildItalic({fileName: 'OpenSans-RegularItalic', weight: '400'})};
    ${buildItalic({fileName: 'OpenSans-LightItalic', weight: '300'})};
  `;
};

export default css`
  ${openSans()}
`;
