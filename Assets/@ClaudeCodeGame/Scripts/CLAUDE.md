# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## プロジェクト概要

Unity ゲームプロジェクト（C#）。スクリプトは `@ClaudeCodeGame/Scripts/` 配下にシーン別で整理されている。

## アーキテクチャ

### シーンフロー
シーンは `GameManager.SCENE_TYPE` enum で定義され、インデックスで読み込まれる:
- `0_StartUp` - 初期化シーン。永続マネージャー（GameManager, SoundManager, FadeManager）を配置
- `1_Title` - タイトル画面（スタート/終了ボタン）
- 追加シーン: MAIN_GAME, GAME, RESULT

### シングルトンマネージャー（DontDestroyOnLoad）
全マネージャーは静的 `GetInstance()` パターンを使用:
- **GameManager** - `LoadScene(SCENE_TYPE)` でシーン遷移
- **SoundManager** - BGM/SE の再生・音量制御
- **FadeManager** - シーン遷移時のフェード演出、シーン読み込み完了時に自動フェードイン

### シーン遷移
遷移には必ず `FadeManager.GetInstance().FadeOutAndLoadScene()` を使用すること。フェードアウト→シーン読み込み→自動フェードインを処理する。

## コーディング規約

### 命名規則
- プライベートフィールド: `m_` プレフィックス（例: `m_Instance`, `m_IsFading`）
- SerializeField: プレフィックスなし PascalCase（例: `FadeDuration`, `StartButton`）
- メソッド引数: `_` プレフィックス + PascalCase（例: `_OnComplete`, `_scene_type`）
- ローカル変数: `var` 型推論を優先
- 定数: UPPER_SNAKE_CASE

### ドキュメント
- public メンバーには日本語で XML ドキュメントコメントを記述
- コメントは日本語で記述（例: `// シングルトンの設定.`）

### ファイル
- C#ファイル作成時は UTF-8（BOM付き）で保存

## 依存ライブラリ

- DOTween (DG.Tweening) - UI アニメーション
- TextMeshPro (TMPro) - テキスト描画

## 応答言語

ユーザーへの応答は日本語で行うこと。
