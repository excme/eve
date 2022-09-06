var gulp = require('gulp');
var sass = require('gulp-sass');
var minifyCSS = require('gulp-clean-css');
var concat = require('gulp-concat');
var sourcemaps = require('gulp-sourcemaps');
var livereload = require('gulp-livereload');
var webserver = require('gulp-webserver');
var download = require('gulp-download-stream');
var header = require('gulp-header');
//var uglify = require('gulp-uglify');
var uglify = require('gulp-uglify-es').default;
var strip = require('gulp-strip-comments');
var merge = require('merge-stream');
//var fileinclude = require('gulp-file-include');
var distPath = 'wwwroot/';
var clean_folder = require('gulp-clean');

// 01. fonts
//gulp.task('fonts', function () {
//	return gulp.src(['node_modules/@fortawesome/fontawesome-free/webfonts/*'])
//		.pipe(gulp.dest(distPath + '/webfonts/'));
//});

var js_libs = [
	'node_modules/jquery/dist/jquery.min.js',
	'node_modules/jquery-slimscroll/jquery.slimscroll.min.js',
];

//var paths = {
//	scripts: ["typescripts/**/*.js", "typescripts/**/*.ts", "typescripts/**/*.map"],
//};

//gulp.task("default", function () {
//	gulp.src(paths.scripts).pipe(gulp.dest("wwwroot/js"));
//});

// 02. js
gulp.task('js', function () {
	//gulp.src(['js/demo/**'])
		//.pipe(gulp.dest(distPath + '/assets/js/demo'));
	//gulp.src(['js/theme/**'])
	//.pipe(gulp.dest(distPath + '/assets/js/theme'));
	return gulp.src(js_array)
		.pipe(concat('s.js'))
		.pipe(strip())
		.pipe(uglify())
		.pipe(gulp.dest(distPath + '/'))
		.pipe(livereload());
});

// 02.1 js-libs
gulp.task('js_libs', function () {
	return gulp.src(js_libs)
		.pipe(concat('js/libs.js'))
		.pipe(strip())
		.pipe(uglify())
		.pipe(gulp.dest(distPath + '/'))
		.pipe(livereload());
});

// 02.1 js-debug
gulp.task('js-debug', function () {
	return gulp.src(js_array)
		//.pipe(sourcemaps.init())
		.pipe(concat('app.debug.js'))
		.pipe(sourcemaps.init())
		.pipe(sourcemaps.write())
		//.pipe(sourcemaps.write())
		.pipe(gulp.dest(distPath + '/'))
		.pipe(livereload());
});

var css_array = [
	//'node_modules/animate.css/animate.min.css',
	//'node_modules/@fortawesome/fontawesome-free/css/all.min.css',
	//'node_modules/jquery-ui-dist/jquery-ui.min.css',
	//'node_modules/pace-js/themes/black/pace-theme-flash.css',
	'wwwroot/_sources/scss/theme.scss',
];

// 06. default-css
gulp.task('css_dev', function () {
	return gulp.src(css_array)
		.pipe(sourcemaps.init())
		.pipe(sass())
		.pipe(concat('app.css'))
		.pipe(sourcemaps.write())
		.pipe(gulp.dest(distPath + '/'))
		.pipe(livereload());
});

gulp.task('css_prod', function () {
	var task_sass = function () {
		return gulp.src(css_array)
			.pipe(sass())
			.pipe(concat('sass.css'))
			.pipe(gulp.dest(distPath + '/'))
			.pipe(livereload());
	};

	var task_min = function () {
		return gulp.src(['wwwroot/dx.common.css', 'wwwroot/dx.light.css', 'wwwroot/sass.css'])
			.pipe(concat('s.css'))
			.pipe(minifyCSS({ debug: true, level: { 1: { specialComments: 0 } } }, (details) => {
				console.log(`${details.name}: ${details.stats.originalSize}`);
				console.log(`${details.name}: ${details.stats.minifiedSize}`);
			}))
			.pipe(gulp.dest(distPath + '/'))
			.pipe(livereload());
	};

	return task_sass().on('end', task_min);
});


// 07. default-css-rtl
//gulp.task('default-css-rtl', function () {
//	return gulp.src([
//		'node_modules/animate.css/animate.min.css',
//		'node_modules/@fortawesome/fontawesome-free/css/all.min.css',
//		'node_modules/jquery-ui-dist/jquery-ui.min.css',
//		'node_modules/pace-js/themes/black/pace-theme-flash.css',
//		'scss/styles.scss'
//	])
//		.pipe(header('$enable-rtl: true;'))
//		.pipe(sass())
//		.pipe(concat('app-rtl.min.css'))
//		.pipe(minifyCSS())
//		.pipe(gulp.dest(distPath + '/assets/css/default/'));
//});

// 08. default-css-theme
//gulp.task('default-css-theme', function () {
//	var colorList = ['red', 'pink', 'orange', 'yellow', 'lime', 'green', 'teal', 'aqua', 'blue', 'purple', 'indigo', 'black'];

//	var tasks = colorList.map(function (color) {
//		return gulp.src(['wwwroot/_sources/scss/theme.scss'])
//			.pipe(header('$primary-color: \'' + color + '\';'))
//			.pipe(sass())
//			.pipe(concat(color + '.min.css'))
//			.pipe(minifyCSS())
//			.pipe(gulp.dest(distPath + '/assets/css/default/theme/'));
//	});
//	console.log('Generating the css files. Please wait...');
//	return merge(tasks);
//});

// 09. default-css-image
//gulp.task('default-css-image', function () {
//	return gulp.src(['scss/images/**'])
//		.pipe(gulp.dest(distPath + '/assets/css/default/images'));
//});

// 10. default-watch
//gulp.task('default-watch', function () {
//	livereload.listen();

//	gulp.watch('html/**/**.html', gulp.series(gulp.parallel(['default-fileinclude'])));
//	gulp.watch('html-startup/**/**.html', gulp.series(gulp.parallel(['default-startup-fileinclude'])));
//	gulp.watch('scss/**/**.scss', gulp.series(gulp.parallel(['default-css', 'default-css-theme'])));
//	gulp.watch('js/*.js', gulp.series(gulp.parallel(['js'])));
//});

// 11. default-webserver
//gulp.task('default-webserver', function () {
//	gulp.src(distPath)
//		.pipe(webserver({
//			host: 'localhost',
//			livereload: true,
//			directoryListing: false,
//			open: '/template_html/',
//			fallback: 'page_blank.html'
//		}));
//});

// 12. default
//gulp.task('default', gulp.series(gulp.parallel(['fonts', 'js', 'default-css', 'default-css-theme', 'default-css-image', 'default-fileinclude', 'default-startup-fileinclude', 'default-webserver', 'default-watch'])));

//// 13. material-fileinclude
//gulp.task('material-fileinclude', function () {
//	return gulp.src(['./html/*.html'])
//		.pipe(fileinclude({
//			prefix: '@@',
//			basepath: '@file',
//			rootPath: './',
//			context: {
//				theme: 'material'
//			}
//		}))
//		.pipe(gulp.dest(distPath + '/template_material'))
//		.pipe(livereload());
//});

//// 14. material-startup-fileinclude
//gulp.task('material-startup-fileinclude', function () {
//	return gulp.src(['./html-startup/*.html'])
//		.pipe(fileinclude({
//			prefix: '@@',
//			basepath: '@file',
//			rootPath: './',
//			context: {
//				theme: 'material'
//			}
//		}))
//		.pipe(gulp.dest(distPath + '/template_material_startup'))
//		.pipe(livereload());
//});

//// 15. material-css
//gulp.task('material-css', function () {
//	return gulp.src([
//		'node_modules/animate.css/animate.min.css',
//		'node_modules/@fortawesome/fontawesome-free/css/all.min.css',
//		'node_modules/jquery-ui-dist/jquery-ui.min.css',
//		'node_modules/pace-js/themes/black/pace-theme-flash.css',
//		'scss/material/styles.scss'
//	])
//		.pipe(sass())
//		.pipe(concat('app.min.css'))
//		.pipe(minifyCSS({ debug: true }, (details) => {
//			console.log(`${details.name}: ${details.stats.originalSize}`);
//			console.log(`${details.name}: ${details.stats.minifiedSize}`);
//		}))
//		.pipe(gulp.dest(distPath + '/assets/css/material/'))
//		.pipe(livereload());
//});

//// 16. material-css-rtl
//gulp.task('material-css-rtl', function () {
//	return gulp.src([
//		'node_modules/animate.css/animate.min.css',
//		'node_modules/@fortawesome/fontawesome-free/css/all.min.css',
//		'node_modules/jquery-ui-dist/jquery-ui.min.css',
//		'node_modules/pace-js/themes/black/pace-theme-flash.css',
//		'scss/material/styles.scss'
//	])
//		.pipe(header('$enable-rtl: true;'))
//		.pipe(sass())
//		.pipe(concat('app-rtl.min.css'))
//		.pipe(minifyCSS())
//		.pipe(gulp.dest(distPath + '/assets/css/material/'));
//});

//// 17. material-css-theme
//gulp.task('material-css-theme', function () {
//	var colorList = ['red', 'pink', 'orange', 'yellow', 'lime', 'green', 'teal', 'aqua', 'blue', 'purple', 'indigo', 'black'];

//	var tasks = colorList.map(function (color) {
//		return gulp.src(['scss/material/theme.scss'])
//			.pipe(header('$primary-color: \'' + color + '\';'))
//			.pipe(sass())
//			.pipe(concat(color + '.min.css'))
//			.pipe(minifyCSS())
//			.pipe(gulp.dest(distPath + '/assets/css/material/theme/'));
//	});
//	console.log('Generating the css files. Please wait...');
//	return merge(tasks);
//});

//// 18. material-css-image
//gulp.task('material-css-image', function () {
//	return gulp.src(['scss/material/images/**'])
//		.pipe(gulp.dest(distPath + '/assets/css/material/images'));
//});

//// 19. material-watch
//gulp.task('material-watch', function () {
//	livereload.listen();
//	gulp.watch('html/**/**.html', gulp.series(gulp.parallel(['material-fileinclude'])));
//	gulp.watch('html-startup/**/**.html', gulp.series(gulp.parallel(['material-startup-fileinclude'])));
//	gulp.watch('scss/**/**.scss', gulp.series(gulp.parallel(['material-css', 'material-css-theme'])));
//	gulp.watch('js/*.js', gulp.series(gulp.parallel(['js'])));
//});

//// 20. material-webserver
//gulp.task('material-webserver', function () {
//	gulp.src(distPath)
//		.pipe(webserver({
//			host: 'localhost',
//			livereload: true,
//			directoryListing: false,
//			open: '/template_material/',
//			fallback: 'page_blank.html'
//		}));
//});

//// 21. material
//gulp.task('material', gulp.series(gulp.parallel(['fonts', 'js', 'material-css', 'material-css-theme', 'material-css-image', 'material-fileinclude', 'material-startup-fileinclude', 'material-webserver', 'material-watch'])));

//// 22. apple-fileinclude
//gulp.task('apple-fileinclude', function () {
//	return gulp.src(['./html/*.html'])
//		.pipe(fileinclude({
//			prefix: '@@',
//			basepath: '@file',
//			rootPath: './',
//			context: {
//				theme: 'apple'
//			}
//		}))
//		.pipe(gulp.dest(distPath + '/template_apple'))
//		.pipe(livereload());
//});

//// 23. apple-startup-fileinclude
//gulp.task('apple-startup-fileinclude', function () {
//	return gulp.src(['./html-startup/*.html'])
//		.pipe(fileinclude({
//			prefix: '@@',
//			basepath: '@file',
//			rootPath: './',
//			context: {
//				theme: 'apple'
//			}
//		}))
//		.pipe(gulp.dest(distPath + '/template_apple_startup'))
//		.pipe(livereload());
//});

//// 24. apple-css
//gulp.task('apple-css', function () {
//	return gulp.src([
//		'node_modules/animate.css/animate.min.css',
//		'node_modules/@fortawesome/fontawesome-free/css/all.min.css',
//		'node_modules/jquery-ui-dist/jquery-ui.min.css',
//		'node_modules/pace-js/themes/black/pace-theme-flash.css',
//		'scss/apple/styles.scss'
//	])
//		.pipe(sass())
//		.pipe(concat('app.min.css'))
//		.pipe(minifyCSS({ debug: true }, (details) => {
//			console.log(`${details.name}: ${details.stats.originalSize}`);
//			console.log(`${details.name}: ${details.stats.minifiedSize}`);
//		}))
//		.pipe(gulp.dest(distPath + '/assets/css/apple/'))
//		.pipe(livereload());
//});

//// 25. apple-css-rtl
//gulp.task('apple-css-rtl', function () {
//	return gulp.src([
//		'node_modules/animate.css/animate.min.css',
//		'node_modules/@fortawesome/fontawesome-free/css/all.min.css',
//		'node_modules/jquery-ui-dist/jquery-ui.min.css',
//		'node_modules/pace-js/themes/black/pace-theme-flash.css',
//		'scss/apple/styles.scss'
//	])
//		.pipe(header('$enable-rtl: true;'))
//		.pipe(sass())
//		.pipe(concat('app-rtl.min.css'))
//		.pipe(minifyCSS())
//		.pipe(gulp.dest(distPath + '/assets/css/apple/'));
//});

//// 26. apple-css-theme
//gulp.task('apple-css-theme', function () {
//	var colorList = ['red', 'pink', 'orange', 'yellow', 'lime', 'green', 'teal', 'aqua', 'blue', 'purple', 'indigo', 'black'];

//	var tasks = colorList.map(function (color) {
//		return gulp.src(['scss/apple/theme.scss'])
//			.pipe(header('$primary-color: \'' + color + '\';'))
//			.pipe(sass())
//			.pipe(concat(color + '.min.css'))
//			.pipe(minifyCSS())
//			.pipe(gulp.dest(distPath + '/assets/css/apple/theme/'));
//	});
//	console.log('Generating the css files. Please wait...');
//	return merge(tasks);
//});

//// 27. apple-css-image
//gulp.task('apple-css-image', function () {
//	return gulp.src(['scss/apple/images/**'])
//		.pipe(gulp.dest(distPath + '/assets/css/apple/images'));
//});

//// 28. apple-watch
//gulp.task('apple-watch', function () {
//	livereload.listen();
//	gulp.watch('html/**/**.html', gulp.series(gulp.parallel(['apple-fileinclude'])));
//	gulp.watch('html-startup/**/**.html', gulp.series(gulp.parallel(['apple-startup-fileinclude'])));
//	gulp.watch('scss/**/**.scss', gulp.series(gulp.parallel(['apple-css', 'apple-css-theme'])));
//	gulp.watch('js/*.js', gulp.series(gulp.parallel(['js'])));
//});

//// 29. apple-webserver
//gulp.task('apple-webserver', function () {
//	gulp.src(distPath)
//		.pipe(webserver({
//			host: 'localhost',
//			livereload: true,
//			directoryListing: false,
//			open: '/template_apple/',
//			fallback: 'page_blank.html'
//		}));
//});

//// 30. apple
//gulp.task('apple', gulp.series(gulp.parallel(['fonts', 'js', 'apple-css', 'apple-css-theme', 'apple-css-image', 'apple-fileinclude', 'apple-startup-fileinclude', 'apple-webserver', 'apple-watch'])));



//// 31. transparent-fileinclude
//gulp.task('transparent-fileinclude', function () {
//	return gulp.src(['./html/*.html'])
//		.pipe(fileinclude({
//			prefix: '@@',
//			basepath: '@file',
//			rootPath: './',
//			context: {
//				theme: 'transparent'
//			}
//		}))
//		.pipe(gulp.dest(distPath + '/template_transparent'))
//		.pipe(livereload());
//});

//// 32. transparent-startup-fileinclude
//gulp.task('transparent-startup-fileinclude', function () {
//	return gulp.src(['./html-startup/*.html'])
//		.pipe(fileinclude({
//			prefix: '@@',
//			basepath: '@file',
//			rootPath: './',
//			context: {
//				theme: 'transparent'
//			}
//		}))
//		.pipe(gulp.dest(distPath + '/template_transparent_startup'))
//		.pipe(livereload());
//});

//// 33. transparent-css
//gulp.task('transparent-css', function () {
//	return gulp.src([
//		'node_modules/animate.css/animate.min.css',
//		'node_modules/@fortawesome/fontawesome-free/css/all.min.css',
//		'node_modules/jquery-ui-dist/jquery-ui.min.css',
//		'node_modules/pace-js/themes/black/pace-theme-flash.css',
//		'scss/transparent/styles.scss'
//	])
//		.pipe(sass())
//		.pipe(concat('app.min.css'))
//		.pipe(minifyCSS({ debug: true }, (details) => {
//			console.log(`${details.name}: ${details.stats.originalSize}`);
//			console.log(`${details.name}: ${details.stats.minifiedSize}`);
//		}))
//		.pipe(gulp.dest(distPath + '/assets/css/transparent/'))
//		.pipe(livereload());
//});

//// 34. transparent-css-rtl
//gulp.task('transparent-css-rtl', function () {
//	return gulp.src([
//		'node_modules/animate.css/animate.min.css',
//		'node_modules/@fortawesome/fontawesome-free/css/all.min.css',
//		'node_modules/jquery-ui-dist/jquery-ui.min.css',
//		'node_modules/pace-js/themes/black/pace-theme-flash.css',
//		'scss/transparent/styles.scss'
//	])
//		.pipe(header('$enable-rtl: true;'))
//		.pipe(sass())
//		.pipe(concat('app-rtl.min.css'))
//		.pipe(minifyCSS())
//		.pipe(gulp.dest(distPath + '/assets/css/transparent/'));
//});

//// 35. transparent-css-theme
//gulp.task('transparent-css-theme', function () {
//	var colorList = ['red', 'pink', 'orange', 'yellow', 'lime', 'green', 'teal', 'aqua', 'blue', 'purple', 'indigo', 'black'];

//	var tasks = colorList.map(function (color) {
//		return gulp.src(['scss/transparent/theme.scss'])
//			.pipe(header('$primary-color: \'' + color + '\';'))
//			.pipe(sass())
//			.pipe(concat(color + '.min.css'))
//			.pipe(minifyCSS())
//			.pipe(gulp.dest(distPath + '/assets/css/transparent/theme/'));
//	});
//	console.log('Generating the css files. Please wait...');
//	return merge(tasks);
//});

//// 36. transparent-css-image
//gulp.task('transparent-css-image', function () {
//	return gulp.src(['scss/transparent/images/**'])
//		.pipe(gulp.dest(distPath + '/assets/css/transparent/images'));
//});

//// 37. transparent-watch
//gulp.task('transparent-watch', function () {
//	livereload.listen();
//	gulp.watch('html/**/**.html', gulp.series(gulp.parallel(['transparent-fileinclude'])));
//	gulp.watch('html-startup/**/**.html', gulp.series(gulp.parallel(['transparent-startup-fileinclude'])));
//	gulp.watch('scss/**/**.scss', gulp.series(gulp.parallel(['transparent-css', 'transparent-css-theme'])));
//	gulp.watch('js/*.js', gulp.series(gulp.parallel(['js'])));
//});

//// 38. transparent-webserver
//gulp.task('transparent-webserver', function () {
//	gulp.src(distPath)
//		.pipe(webserver({
//			host: 'localhost',
//			livereload: true,
//			directoryListing: false,
//			open: '/template_transparent/',
//			fallback: 'page_blank.html'
//		}));
//});

//// 39. transparent
//gulp.task('transparent', gulp.series(gulp.parallel(['fonts', 'js', 'transparent-css', 'transparent-css-theme', 'transparent-css-image', 'transparent-fileinclude', 'transparent-startup-fileinclude', 'transparent-webserver', 'transparent-watch'])));



//// 40. facebook-fileinclude
//gulp.task('facebook-fileinclude', function () {
//	return gulp.src(['./html/*.html'])
//		.pipe(fileinclude({
//			prefix: '@@',
//			basepath: '@file',
//			rootPath: './',
//			context: {
//				theme: 'facebook'
//			}
//		}))
//		.pipe(gulp.dest(distPath + '/template_facebook'))
//		.pipe(livereload());
//});

//// 41. facebook-startup-fileinclude
//gulp.task('facebook-startup-fileinclude', function () {
//	return gulp.src(['./html-startup/*.html'])
//		.pipe(fileinclude({
//			prefix: '@@',
//			basepath: '@file',
//			rootPath: './',
//			context: {
//				theme: 'facebook'
//			}
//		}))
//		.pipe(gulp.dest(distPath + '/template_facebook_startup'))
//		.pipe(livereload());
//});

//// 42. facebook-css
//gulp.task('facebook-css', function () {
//	return gulp.src([
//		'node_modules/animate.css/animate.min.css',
//		'node_modules/@fortawesome/fontawesome-free/css/all.min.css',
//		'node_modules/jquery-ui-dist/jquery-ui.min.css',
//		'node_modules/pace-js/themes/black/pace-theme-flash.css',
//		'scss/facebook/styles.scss'
//	])
//		.pipe(sass())
//		.pipe(concat('app.min.css'))
//		.pipe(minifyCSS({ debug: true }, (details) => {
//			console.log(`${details.name}: ${details.stats.originalSize}`);
//			console.log(`${details.name}: ${details.stats.minifiedSize}`);
//		}))
//		.pipe(gulp.dest(distPath + '/assets/css/facebook/'))
//		.pipe(livereload());
//});

//// 43. facebook-css-rtl
//gulp.task('facebook-css-rtl', function () {
//	return gulp.src([
//		'node_modules/animate.css/animate.min.css',
//		'node_modules/@fortawesome/fontawesome-free/css/all.min.css',
//		'node_modules/jquery-ui-dist/jquery-ui.min.css',
//		'node_modules/pace-js/themes/black/pace-theme-flash.css',
//		'scss/facebook/styles.scss'
//	])
//		.pipe(header('$enable-rtl: true;'))
//		.pipe(sass())
//		.pipe(concat('app-rtl.min.css'))
//		.pipe(minifyCSS())
//		.pipe(gulp.dest(distPath + '/assets/css/facebook/'));
//});

//// 44. facebook-css-theme
//gulp.task('facebook-css-theme', function () {
//	var colorList = ['red', 'pink', 'orange', 'yellow', 'lime', 'green', 'teal', 'aqua', 'blue', 'purple', 'indigo', 'black'];

//	var tasks = colorList.map(function (color) {
//		return gulp.src(['scss/facebook/theme.scss'])
//			.pipe(header('$primary-color: \'' + color + '\';'))
//			.pipe(sass())
//			.pipe(concat(color + '.min.css'))
//			.pipe(minifyCSS())
//			.pipe(gulp.dest(distPath + '/assets/css/facebook/theme/'));
//	});
//	console.log('Generating the css files. Please wait...');
//	return merge(tasks);
//});

//// 45. facebook-css-image
//gulp.task('facebook-css-image', function () {
//	return gulp.src(['scss/facebook/images/**'])
//		.pipe(gulp.dest(distPath + '/assets/css/facebook/images'));
//});

//// 46. facebook-watch
//gulp.task('facebook-watch', function () {
//	livereload.listen();
//	gulp.watch('html/**/**.html', gulp.series(gulp.parallel(['facebook-fileinclude'])));
//	gulp.watch('html-startup/**/**.html', gulp.series(gulp.parallel(['facebook-startup-fileinclude'])));
//	gulp.watch('scss/**/**.scss', gulp.series(gulp.parallel(['facebook-css', 'facebook-css-theme'])));
//	gulp.watch('js/*.js', gulp.series(gulp.parallel(['js'])));
//});

//// 47. facebook-webserver
//gulp.task('facebook-webserver', function () {
//	gulp.src(distPath)
//		.pipe(webserver({
//			host: 'localhost',
//			livereload: true,
//			directoryListing: false,
//			open: '/template_facebook/',
//			fallback: 'page_blank.html'
//		}));
//});

//// 48. facebook
//gulp.task('facebook', gulp.series(gulp.parallel(['fonts', 'js', 'facebook-css', 'facebook-css-theme', 'facebook-css-image', 'facebook-fileinclude', 'facebook-startup-fileinclude', 'facebook-webserver', 'facebook-watch'])));



//// 49. google-fileinclude
//gulp.task('google-fileinclude', function () {
//	return gulp.src(['./html/*.html'])
//		.pipe(fileinclude({
//			prefix: '@@',
//			basepath: '@file',
//			rootPath: './',
//			context: {
//				theme: 'google'
//			}
//		}))
//		.pipe(gulp.dest(distPath + '/template_google'))
//		.pipe(livereload());
//});

//// 50. google-startup-html
//gulp.task('google-startup-fileinclude', function () {
//	return gulp.src(['./html-startup/*.html'])
//		.pipe(fileinclude({
//			prefix: '@@',
//			basepath: '@file',
//			rootPath: './',
//			context: {
//				theme: 'google'
//			}
//		}))
//		.pipe(gulp.dest(distPath + '/template_google_startup'))
//		.pipe(livereload());
//});

//// 51. google-css
//gulp.task('google-css', function () {
//	return gulp.src([
//		'node_modules/animate.css/animate.min.css',
//		'node_modules/@fortawesome/fontawesome-free/css/all.min.css',
//		'node_modules/jquery-ui-dist/jquery-ui.min.css',
//		'node_modules/pace-js/themes/black/pace-theme-flash.css',
//		'scss/google/styles.scss'
//	])
//		.pipe(sass())
//		.pipe(concat('app.min.css'))
//		.pipe(minifyCSS({ debug: true }, (details) => {
//			console.log(`${details.name}: ${details.stats.originalSize}`);
//			console.log(`${details.name}: ${details.stats.minifiedSize}`);
//		}))
//		.pipe(gulp.dest(distPath + '/assets/css/google/'))
//		.pipe(livereload());
//});

//// 52. google-css-rtl
//gulp.task('google-css-rtl', function () {
//	return gulp.src([
//		'node_modules/animate.css/animate.min.css',
//		'node_modules/@fortawesome/fontawesome-free/css/all.min.css',
//		'node_modules/jquery-ui-dist/jquery-ui.min.css',
//		'node_modules/pace-js/themes/black/pace-theme-flash.css',
//		'scss/google/styles.scss'
//	])
//		.pipe(header('$enable-rtl: true;'))
//		.pipe(sass())
//		.pipe(concat('app-rtl.min.css'))
//		.pipe(minifyCSS())
//		.pipe(gulp.dest(distPath + '/assets/css/google/'));
//});

//// 53. google-css-theme
//gulp.task('google-css-theme', function () {
//	var colorList = ['red', 'pink', 'orange', 'yellow', 'lime', 'green', 'teal', 'aqua', 'blue', 'purple', 'indigo', 'black'];

//	var tasks = colorList.map(function (color) {
//		return gulp.src(['scss/google/theme.scss'])
//			.pipe(header('$primary-color: \'' + color + '\';'))
//			.pipe(sass())
//			.pipe(concat(color + '.min.css'))
//			.pipe(minifyCSS())
//			.pipe(gulp.dest(distPath + '/assets/css/google/theme/'));
//	});
//	console.log('Generating the css files. Please wait...');
//	return merge(tasks);
//});

//// 54. google-css-image
//gulp.task('google-css-image', function () {
//	return gulp.src(['scss/google/images/**'])
//		.pipe(gulp.dest(distPath + '/assets/css/google/images'));
//});

//// 55. google-watch
//gulp.task('google-watch', function () {
//	livereload.listen();
//	gulp.watch('html/**/**.html', gulp.series(gulp.parallel(['google-fileinclude'])));
//	gulp.watch('html-startup/**/**.html', gulp.series(gulp.parallel(['google-startup-fileinclude'])));
//	gulp.watch('scss/**/**.scss', gulp.series(gulp.parallel(['google-css', 'google-css-theme'])));
//	gulp.watch('js/*.js', gulp.series(gulp.parallel(['js'])));
//});

//// 56. google-webserver
//gulp.task('google-webserver', function () {
//	gulp.src(distPath)
//		.pipe(webserver({
//			host: 'localhost',
//			livereload: true,
//			directoryListing: false,
//			open: '/template_google/',
//			fallback: 'page_blank.html'
//		}));
//});

//// 57. google
//gulp.task('google', gulp.series(gulp.parallel(['fonts', 'js', 'google-css', 'google-css-theme', 'google-css-image', 'google-fileinclude', 'google-startup-fileinclude', 'google-webserver', 'google-watch'])));

/*
	TASK LIST
	------------------
	01. fonts
	02. js
	03. plugins

	04. default-fileinclude
	05. default-startup-fileinclude
	06. default-css
	07. default-css-rtl
	08. default-css-theme
	09. default-css-image
	10. default-watch
	11. default-webserver
	12. default

	13. material-fileinclude
	14. material-startup-fileinclude
	15. material-css
	16. material-css-rtl
	17. material-css-theme
	18. material-css-image
	19. material-watch
	20. material-webserver
	21. material

	22. apple-fileinclude
	23. apple-startup-fileinclude
	24. apple-css
	25. apple-css-rtl
	26. apple-css-theme
	27. apple-css-image
	28. apple-watch
	29. apple-webserver
	30. apple

	31. transparent-fileinclude
	32. transparent-startup-fileinclude
	33. transparent-css
	34. transparent-css-rtl
	35. transparent-css-theme
	36. transparent-css-image
	37. transparent-watch
	38. transparent-webserver
	39. transparent

	40. facebook-fileinclude
	41. facebook-startup-fileinclude
	42. facebook-css
	43. facebook-css-rtl
	44. facebook-css-theme
	45. facebook-css-image
	46. facebook-watch
	47. facebook-webserver
	48. facebook

	49. google-fileinclude
	50. google-startup-fileinclude
	51. google-css
	52. google-css-rtl
	53. google-css-theme
	54. google-css-image
	55. google-watch
	56. google-webserver
	57. google
*/