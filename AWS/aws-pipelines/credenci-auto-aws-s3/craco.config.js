/* eslint-disable @typescript-eslint/no-var-requires */
const path = require('path');
const fastRefreshCracoPlugin = require('craco-fast-refresh');

module.exports = {
  plugins: [{ plugin: fastRefreshCracoPlugin }],
  webpack: {
    alias: {
      '~/': path.resolve(__dirname, 'src/'),
      '~/app': path.resolve(__dirname, 'src/app'),
      '~/clients': path.resolve(__dirname, 'src/clients'),
      '~/components': path.resolve(__dirname, 'src/components'),
      '~/consts': path.resolve(__dirname, 'src/consts'),
      '~/pages': path.resolve(__dirname, 'src/pages'),
      '~/partials': path.resolve(__dirname, 'src/partials'),
      '~/services': path.resolve(__dirname, 'src/services'),
      '~/stores': path.resolve(__dirname, 'src/stores'),
      '~/styled': path.resolve(__dirname, 'src/styled'),
      '~/theme': path.resolve(__dirname, 'src/theme'),
      '~/utils': path.resolve(__dirname, 'src/utils'),
    },
  },
};
